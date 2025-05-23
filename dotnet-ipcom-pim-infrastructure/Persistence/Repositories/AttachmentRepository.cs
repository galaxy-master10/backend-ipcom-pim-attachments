using dotnet_ipcom_pim_domain.DTOs.Custom;
using dotnet_ipcom_pim_domain.DTOs.Filters;
using dotnet_ipcom_pim_domain.Entities;
using dotnet_ipcom_pim_domain.Interfaces;
using dotnet_ipcom_pim_infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace dotnet_ipcom_pim_infrastructure.Persistence.Repositories;

public class AttachmentRepository : IAttachmentRepository
{
    private readonly PimDbContext _context;
    
    public AttachmentRepository(PimDbContext context)
    {
        _context = context;
    }
    
    public async Task<AttachmentDTOForById?> GetAttachmentByIdAsync(Guid id)
    {
        var attachment = await _context.Attachments
            .Include(a => a.Products)
            .Include(a => a.Countries)
            .Include(a => a.AttachmentCategories)
            // Remove: .ThenInclude(ac => ac.Translations)
            .AsSplitQuery()
            .FirstOrDefaultAsync(a => a.Id == id);

        if (attachment == null) 
            return null;

        // Load translations for the categories
        var categoryIds = attachment.AttachmentCategories.Select(ac => ac.Id).ToList();
        var translations = await _context.Translations
            .Where(t => categoryIds.Contains(t.TranslatableId) && t.Property == "Name")
            .ToListAsync();

        // Group translations by TranslatableId for efficient lookup
        var translationsByCategory = translations
            .GroupBy(t => t.TranslatableId)
            .ToDictionary(g => g.Key, g => g.ToList());

        return new AttachmentDTOForById
        {
            Id = attachment.Id,
            Name = attachment.Name,
            LanguageCode = attachment.LanguageCode,
            Published = attachment.Published,
            Index = attachment.Index,
            NoResize = attachment.NoResize,
            Size = attachment.Size,
            ExpiryDate = attachment.ExpiryDate,
            Md5 = attachment.Md5,
            Content = attachment.Content,
            CountryNames = attachment.Countries.Select(c => c.Name).ToList(),
            Products = attachment.Products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name
            }).ToList(),
            CategoryNames = attachment.AttachmentCategories.Select(ac =>
            {
                if (translationsByCategory.TryGetValue(ac.Id, out var categoryTranslations))
                {
                    // Try to find translation in attachment's language
                    var translation = categoryTranslations
                        .FirstOrDefault(t => t.LanguageCode == attachment.LanguageCode)
                        ?? categoryTranslations.FirstOrDefault();
                    
                    return translation?.LanguageTranslation;
                }
                return null;
            }).Where(name => name != null).ToList()!
        };
    }
    
    public async Task<(List<AttachmentDTO> Attachments, int TotalCount, int ExpiringWithin7Days, int ExpiringWithin30Days)> GetAttachmentsAsync(
        AttachmentFilterDTO filter, int page = 1, int pageSize = 10)
    {
        page = page <= 0 ? 1 : page;
        pageSize = pageSize <= 0 ? 10 : pageSize;

        var query = _context.Attachments
            .Include(a => a.Products)
            .Include(a => a.AttachmentCategories)
            .Include(a => a.Countries)
            // Remove: .ThenInclude(ac => ac.Translations)
            .AsSplitQuery()
            .AsQueryable();
        
        // Apply filters
        if (filter.Id.HasValue)
        {
            query = query.Where(a => a.Id == filter.Id);
        }

        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            query = query.Where(a => a.Name.Contains(filter.Name));
        }
        
        if (!string.IsNullOrWhiteSpace(filter.LanguageCode))
        {
            query = query.Where(a => a.LanguageCode == filter.LanguageCode);
        }
        
        if (filter.Published.HasValue)
        {
            query = query.Where(a => a.Published == filter.Published);
        }

        if (filter.NoResize.HasValue)
        {
            query = query.Where(a => a.NoResize == filter.NoResize);
        }
        
        if (filter.ProductId.HasValue)
        {
            query = query.Where(a => a.Products.Any(p => p.Id == filter.ProductId));
        }
        
        if (filter.Index.HasValue)
        {
            query = query.Where(a => a.Index == filter.Index);
        }

        if (filter.ExpiryDateFrom.HasValue)
        {
            var from = filter.ExpiryDateFrom.Value;
            query = query.Where(a => a.ExpiryDate.HasValue && a.ExpiryDate.Value >= from);
        }

        if (filter.ExpiryDateTo.HasValue)
        {
            var to = filter.ExpiryDateTo.Value;
            query = query.Where(a => a.ExpiryDate.HasValue && a.ExpiryDate.Value <= to);
        }
        
        // For category name filter, we need to join with translations
        if (!string.IsNullOrWhiteSpace(filter.CategoryName))
        {
            var catFilter = filter.CategoryName.ToLower();
            
            // Get category IDs that have matching translations
            var matchingCategoryIds = await _context.Translations
                .Where(t => t.Property == "Name" && 
                           t.LanguageTranslation.ToLower().Contains(catFilter))
                .Select(t => t.TranslatableId)
                .Distinct()
                .ToListAsync();
            
            query = query.Where(a => 
                a.AttachmentCategories.Any(ac => matchingCategoryIds.Contains(ac.Id))
            );
        }
        
        // Get total count after filtering
        var totalCount = await query.CountAsync();
        
        // Calculate expiring counts
        var today = DateTime.Today;
        var sevenDaysFromNow = today.AddDays(7);
        var thirtyDaysFromNow = today.AddDays(30);

        var expiringWithin7Days = await _context.Attachments
            .Where(a => a.ExpiryDate.HasValue &&
                        a.ExpiryDate.Value >= DateOnly.FromDateTime(today) &&
                        a.ExpiryDate.Value <= DateOnly.FromDateTime(sevenDaysFromNow))
            .CountAsync();

        var expiringWithin30Days = await _context.Attachments
            .Where(a => a.ExpiryDate.HasValue &&
                        a.ExpiryDate.Value >= DateOnly.FromDateTime(today) &&
                        a.ExpiryDate.Value <= DateOnly.FromDateTime(thirtyDaysFromNow))
            .CountAsync();
        
        // Get the attachments for the current page
        var attachments = await query
            .OrderByDescending(a => a.ExpiryDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        // Get all unique category IDs from the attachments
        var categoryIds = attachments
            .SelectMany(a => a.AttachmentCategories)
            .Select(ac => ac.Id)
            .Distinct()
            .ToList();

        // Load translations for all categories in one query
        var translations = await _context.Translations
            .Where(t => categoryIds.Contains(t.TranslatableId) && t.Property == "Name")
            .ToListAsync();

        // Group translations by TranslatableId for efficient lookup
        var translationsByCategory = translations
            .GroupBy(t => t.TranslatableId)
            .ToDictionary(g => g.Key, g => g.ToList());

        // Map to DTOs
        var attachmentDtos = attachments.Select(a => new AttachmentDTO
        {
            Id = a.Id,
            Name = a.Name,
            LanguageCode = a.LanguageCode,
            Published = a.Published,
            Index = a.Index,
            NoResize = a.NoResize,
            Size = a.Size,
            ExpiryDate = a.ExpiryDate,
            Products = a.Products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name
            }).ToList(),
            CountryNames = a.Countries.Select(c => c.Name).ToList(),
            CategoryNames = a.AttachmentCategories.Select(ac =>
            {
                if (translationsByCategory.TryGetValue(ac.Id, out var categoryTranslations))
                {
                    // Try to find translation in attachment's language
                    var translation = categoryTranslations
                        .FirstOrDefault(t => t.LanguageCode == a.LanguageCode)
                        ?? categoryTranslations.FirstOrDefault();
                    
                    return translation?.LanguageTranslation;
                }
                return null;
            }).Where(name => name != null).ToList()!
        }).ToList();
        
        return (attachmentDtos, totalCount, expiringWithin7Days, expiringWithin30Days);
    }
    
    public async Task<List<AttachmentDTO>> GetAttachmentsForConsoleAppAsync()
    {
        var today = DateTime.Today;
        var fourWeeksFromNow = today.AddDays(28);
        
        var query = _context.Attachments
            .Include(a => a.Products)
            .Include(a => a.AttachmentCategories)
            .Include(a => a.Countries)
            // Remove: .ThenInclude(ac => ac.Translations)
            .AsSplitQuery()
            .Where(a => a.ExpiryDate.HasValue && 
                        a.ExpiryDate.Value >= DateOnly.FromDateTime(today) && 
                        a.ExpiryDate.Value <= DateOnly.FromDateTime(fourWeeksFromNow))
            .AsQueryable();

        var attachments = await query
            .OrderByDescending(a => a.ExpiryDate)
            .ToListAsync();

        // Get all unique category IDs
        var categoryIds = attachments
            .SelectMany(a => a.AttachmentCategories)
            .Select(ac => ac.Id)
            .Distinct()
            .ToList();

        // Load translations for all categories
        var translations = await _context.Translations
            .Where(t => categoryIds.Contains(t.TranslatableId) && t.Property == "Name")
            .ToListAsync();

        // Group translations by category ID
        var translationsByCategory = translations
            .GroupBy(t => t.TranslatableId)
            .ToDictionary(g => g.Key, g => g.ToList());

        // Map to DTOs
        return attachments.Select(a => new AttachmentDTO
        {
            Id = a.Id,
            Name = a.Name,
            LanguageCode = a.LanguageCode,
            Published = a.Published,
            Index = a.Index,
            NoResize = a.NoResize,
            Size = a.Size,
            ExpiryDate = a.ExpiryDate,
            Products = a.Products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name
            }).ToList(),
            CountryNames = a.Countries.Select(c => c.Name).ToList(),
            CategoryNames = a.AttachmentCategories.Select(ac =>
            {
                if (translationsByCategory.TryGetValue(ac.Id, out var categoryTranslations))
                {
                    var translation = categoryTranslations
                        .FirstOrDefault(t => t.LanguageCode == a.LanguageCode)
                        ?? categoryTranslations.FirstOrDefault();
                    
                    return translation?.LanguageTranslation;
                }
                return null;
            }).Where(name => name != null).ToList()!
        }).ToList();
    }

    public Task<List<Country>> GetAllAttachmentsCountries()
    {
        return _context.Countries.ToListAsync();
    }

    public async Task<List<AttachmentCategory>> GetAllAttachmentsCategories()
    {
        var categories = await _context.AttachmentCategories.ToListAsync();
        
        // If you need to load translations for the categories
        var categoryIds = categories.Select(c => c.Id).ToList();
        var translations = await _context.Translations
            .Where(t => categoryIds.Contains(t.TranslatableId))
            .ToListAsync();
        
        // Group translations by category
        var translationsByCategory = translations.GroupBy(t => t.TranslatableId);
        
        // Manually populate the translations for each category if needed
        foreach (var category in categories)
        {
            var categoryTranslations = translationsByCategory
                .FirstOrDefault(g => g.Key == category.Id);
            
            if (categoryTranslations != null)
            {
                // If your AttachmentCategory entity still has the Translations property
                // and you want to populate it for compatibility:
                category.Translations = categoryTranslations.ToList();
            }
        }
        
        return categories;
    }

    public async Task<bool> UpdateAttachmentAsync(AttachmentDTOForById attachmentDto)
    {
        var attachment = await _context.Attachments
            .Include(a => a.Products)
            .Include(a => a.Countries)
            .Include(a => a.AttachmentCategories)
            // Remove: .ThenInclude(ac => ac.Translations)
            .FirstOrDefaultAsync(a => a.Id == attachmentDto.Id);
        
        if (attachment == null)
        {
            return false;
        }
        
        // Update basic properties
        attachment.Name = attachmentDto.Name;
        attachment.LanguageCode = attachmentDto.LanguageCode;
        attachment.Published = attachmentDto.Published;
        attachment.Index = attachmentDto.Index;
        attachment.NoResize = attachmentDto.NoResize;
        attachment.Size = attachmentDto.Size;
        attachment.ExpiryDate = attachmentDto.ExpiryDate;
        attachment.Md5 = attachmentDto.Md5;
        attachment.Content = attachmentDto.Content;
        
        // Update countries
        attachment.Countries.Clear();
        foreach (var countryName in attachmentDto.CountryNames)
        {
            var country = await _context.Countries
                .FirstOrDefaultAsync(c => c.Name == countryName);
            if (country != null)
            {
                attachment.Countries.Add(country);
            }
        }
        
        // Update categories
        attachment.AttachmentCategories.Clear();
        foreach (var categoryName in attachmentDto.CategoryNames)
        {
            // Find category by its translation
            var categoryId = await _context.Translations
                .Where(t => t.Property == "Name" && 
                           t.LanguageTranslation == categoryName)
                .Select(t => t.TranslatableId)
                .FirstOrDefaultAsync();
            
            if (categoryId != Guid.Empty)
            {
                var category = await _context.AttachmentCategories
                    .FirstOrDefaultAsync(ac => ac.Id == categoryId);
                    
                if (category != null)
                {
                    attachment.AttachmentCategories.Add(category);
                }
            }
        }
        
        _context.Attachments.Update(attachment);
        
        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public Task<List<Language>> GetAllLanguages()
    {
        return _context.Languages.ToListAsync();
    }
}