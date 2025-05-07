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
            .ThenInclude(ac => ac.Translations)
            .AsSplitQuery()
            .FirstOrDefaultAsync(a => a.Id == id);

        return attachment == null ? null : new AttachmentDTOForById
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
                ac.Translations
                    .Where(t => t.Property == "Name" && t.LanguageCode == attachment.LanguageCode)
                    .Select(t => t.LanguageTranslation)
                    .FirstOrDefault()
                ?? ac.Translations
                    .Where(t => t.Property == "Name")
                    .Select(t => t.LanguageTranslation)
                    .FirstOrDefault()
            ).Where(name => name != null).ToList()!
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
            .ThenInclude(ac => ac.Translations)
            .AsSplitQuery()
            .AsQueryable();
        
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
        
        if (!string.IsNullOrWhiteSpace(filter.CategoryName))
        {
            var catFilter = filter.CategoryName.ToLower();
            query = query.Where(a =>
                a.AttachmentCategories.Any(ac =>
                    ac.Translations.Any(t =>
                        t.Property == "Name" &&
                        t.LanguageTranslation.ToLower().Contains(catFilter)
                    )
                )
            );
        }
        
        var totalCount = await query.CountAsync();
        
        // Calculate expiring attachments counts
        var today = DateTime.Today;
        var sevenDaysFromNow = today.AddDays(7);
        var thirtyDaysFromNow = today.AddDays(30);

        var expiringWithin7Days = await _context.Attachments
            .Where(a => a.ExpiryDate.HasValue &&
                        a.ExpiryDate.Value >= DateOnly.FromDateTime(today) &&
                        a.ExpiryDate.Value <= DateOnly.FromDateTime(sevenDaysFromNow))
            .CountAsync<Attachment>();

        var expiringWithin30Days = await _context.Attachments
            .Where(a => a.ExpiryDate.HasValue &&
                        a.ExpiryDate.Value >= DateOnly.FromDateTime(today) &&
                        a.ExpiryDate.Value <= DateOnly.FromDateTime(thirtyDaysFromNow))
            .CountAsync<Attachment>();
        
        var attachments = await query
            .OrderByDescending(a => a.ExpiryDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(a => new AttachmentDTO
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
                CountryNames = a.Countries.Select(
                c => c.Name).ToList(),
                CategoryNames = a.AttachmentCategories.Select(ac =>
                    // For each category, choose a translation for "Name" matching the attachment's language,
                    // otherwise take the first available translation.
                    ac.Translations
                        .Where(t => t.Property == "Name" && t.LanguageCode == a.LanguageCode)
                        .Select(t => t.LanguageTranslation)
                        .FirstOrDefault()
                    ?? ac.Translations
                        .Where(t => t.Property == "Name")
                        .Select(t => t.LanguageTranslation)
                        .FirstOrDefault()
                ).Where(name => name != null).ToList()!
            })
            .ToListAsync();
        


        
        return (attachments, totalCount, expiringWithin7Days, expiringWithin30Days);
    }
    
    public async Task<List<AttachmentDTO>> GetAttachmentsForConsoleAppAsync()
    {
        var today = DateTime.Today;
        var fourWeeksFromNow = today.AddDays(28);
        
        var query = _context.Attachments
            .Include(a => a.Products)
            .Include(a => a.AttachmentCategories)
            .ThenInclude(ac => ac.Translations)
            .AsSplitQuery()
            .Where(a => a.ExpiryDate.HasValue && 
                        a.ExpiryDate.Value >= DateOnly.FromDateTime(today) && 
                        a.ExpiryDate.Value <= DateOnly.FromDateTime(fourWeeksFromNow))
            .AsQueryable();

        var attachments = await query
            .OrderByDescending(a => a.ExpiryDate)
            .Select(a => new AttachmentDTO
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
                })
                .ToList(),
                CategoryNames = a.AttachmentCategories.Select(ac =>
                    // For each category, choose a translation for "Name" matching the attachment's language,
                    // otherwise take the first available translation.
                    ac.Translations
                        .Where(t => t.Property == "Name" && t.LanguageCode == a.LanguageCode)
                        .Select(t => t.LanguageTranslation)
                        .FirstOrDefault()
                    ?? ac.Translations
                        .Where(t => t.Property == "Name")
                        .Select(t => t.LanguageTranslation)
                        .FirstOrDefault()
                ).Where(name => name != null).ToList()!
            })
            .ToListAsync();


        return attachments;
    }

    public Task<List<AttachmentCountry>> GetAllAttachmentsCountries()
    {
        return _context.AttachmentCountries.ToListAsync();
    }

    public Task<List<AttachmentCategory>> GetAllAttachmentsCategories()
    {
        return _context.AttachmentCategories
            .Include(ac => ac.Translations)
            .ToListAsync();
    }
}