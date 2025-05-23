using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_ipcom_pim_domain.DTOs.Custom;
using dotnet_ipcom_pim_domain.DTOs.Filters;
using dotnet_ipcom_pim_domain.Entities;
using dotnet_ipcom_pim_domain.Interfaces;
using dotnet_ipcom_pim_infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace dotnet_ipcom_pim_infrastructure.Persistence.Repositories
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly PimDbContext _context;
        public AttachmentRepository(PimDbContext context) => _context = context;

        public async Task<AttachmentDTOForById?> GetAttachmentByIdAsync(Guid id)
        {
            // 1) load attachment with its navs (no Translations)
            var a = await _context.Attachments
                .Include(x => x.Products)
                .Include(x => x.Countries)
                .Include(x => x.AttachmentCategories)
                .AsSplitQuery()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (a == null) return null;

            // 2) pull down *all* Name-translations
            var allNameTranslations = await _context.Translations
                .Where(t => t.Property == "Name")
                .ToListAsync();

            // 3) project
            return new AttachmentDTOForById
            {
                Id           = a.Id,
                Name         = a.Name,
                LanguageCode = a.LanguageCode,
                Published    = a.Published,
                Index        = a.Index,
                NoResize     = a.NoResize,
                Size         = a.Size,
                ExpiryDate   = a.ExpiryDate,
                Md5          = a.Md5,
                Content      = a.Content,
                CountryNames = a.Countries.Select(c => c.Name).ToList(),
                Products     = a.Products
                                  .Select(p => new ProductDTO { Id = p.Id, Name = p.Name })
                                  .ToList(),
                CategoryNames = a.AttachmentCategories
                    .Select(cat =>
                        // in-memory lookup, first by matching language
                        allNameTranslations
                          .Where(t => t.TranslatableId == cat.Id
                                   && t.LanguageCode   == a.LanguageCode)
                          .Select(t => t.LanguageTranslation)
                          .FirstOrDefault()
                      // fallback to any language
                      ?? allNameTranslations
                          .Where(t => t.TranslatableId == cat.Id)
                          .Select(t => t.LanguageTranslation)
                          .FirstOrDefault()
                    )
                    .Where(n => n != null)
                    .ToList()!
            };
        }

        public async Task<(List<AttachmentDTO> Attachments, int TotalCount, int ExpiringWithin7Days, int ExpiringWithin30Days)>
            GetAttachmentsAsync(AttachmentFilterDTO filter, int page = 1, int pageSize = 10)
        {
            page     = Math.Max(1, page);
            pageSize = Math.Max(1, pageSize);

            // build base filter
            var baseQ = _context.Attachments.AsQueryable();
            if (filter.Id.HasValue)                 baseQ = baseQ.Where(a => a.Id == filter.Id);
            if (!string.IsNullOrWhiteSpace(filter.Name))
                                                     baseQ = baseQ.Where(a => a.Name.Contains(filter.Name));
            if (!string.IsNullOrWhiteSpace(filter.LanguageCode))
                                                     baseQ = baseQ.Where(a => a.LanguageCode == filter.LanguageCode);
            if (filter.Published.HasValue)          baseQ = baseQ.Where(a => a.Published == filter.Published);
            if (filter.NoResize.HasValue)           baseQ = baseQ.Where(a => a.NoResize == filter.NoResize);
            if (filter.ProductId.HasValue)          baseQ = baseQ.Where(a => a.Products.Any(p => p.Id == filter.ProductId));
            if (filter.Index.HasValue)              baseQ = baseQ.Where(a => a.Index == filter.Index);
            if (filter.ExpiryDateFrom.HasValue)     baseQ = baseQ.Where(a => a.ExpiryDate >= filter.ExpiryDateFrom);
            if (filter.ExpiryDateTo.HasValue)       baseQ = baseQ.Where(a => a.ExpiryDate <= filter.ExpiryDateTo);

            var totalCount = await baseQ.CountAsync();

            // expiry stats
            var today = DateTime.Today;
            var d0    = DateOnly.FromDateTime(today);
            var d7    = DateOnly.FromDateTime(today.AddDays(7));
            var d30   = DateOnly.FromDateTime(today.AddDays(30));

            var exp7  = await _context.Attachments
                .Where(a => a.ExpiryDate >= d0 && a.ExpiryDate <= d7)
                .CountAsync();
            var exp30 = await _context.Attachments
                .Where(a => a.ExpiryDate >= d0 && a.ExpiryDate <= d30)
                .CountAsync();

            // data query with includes
            var data = await baseQ
                .Include(a => a.Products)
                .Include(a => a.Countries)
                .Include(a => a.AttachmentCategories)
                .AsSplitQuery()
                .OrderByDescending(a => a.ExpiryDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // pull down all Name-translations in one simple SQL
            var allNameTranslations = await _context.Translations
                .Where(t => t.Property == "Name")
                .ToListAsync();

            // project DTOs
            var dtos = data.Select(a => new AttachmentDTO
            {
                Id           = a.Id,
                Name         = a.Name,
                LanguageCode = a.LanguageCode,
                Published    = a.Published,
                Index        = a.Index,
                NoResize     = a.NoResize,
                Size         = a.Size,
                ExpiryDate   = a.ExpiryDate,
                Products     = a.Products
                                  .Select(p => new ProductDTO { Id = p.Id, Name = p.Name })
                                  .ToList(),
                CountryNames = a.Countries.Select(c => c.Name).ToList(),
                CategoryNames = a.AttachmentCategories
                    .Select(cat =>
                        allNameTranslations
                          .Where(t => t.TranslatableId == cat.Id 
                                   && t.LanguageCode   == a.LanguageCode)
                          .Select(t => t.LanguageTranslation)
                          .FirstOrDefault()
                      ?? allNameTranslations
                          .Where(t => t.TranslatableId == cat.Id)
                          .Select(t => t.LanguageTranslation)
                          .FirstOrDefault()
                    )
                    .Where(n => n != null)
                    .ToList()!
            }).ToList();

            return (dtos, totalCount, exp7, exp30);
        }

        public async Task<List<AttachmentDTO>> GetAttachmentsForConsoleAppAsync()
        {
            var today = DateTime.Today;
            var d0    = DateOnly.FromDateTime(today);
            var d4wk  = DateOnly.FromDateTime(today.AddDays(28));

            var data = await _context.Attachments
                .Include(a => a.Products)
                .Include(a => a.AttachmentCategories)
                .AsSplitQuery()
                .Where(a => a.ExpiryDate >= d0 && a.ExpiryDate <= d4wk)
                .OrderByDescending(a => a.ExpiryDate)
                .ToListAsync();

            var allNameTranslations = await _context.Translations
                .Where(t => t.Property == "Name")
                .ToListAsync();

            return data.Select(a => new AttachmentDTO
            {
                Id           = a.Id,
                Name         = a.Name,
                LanguageCode = a.LanguageCode,
                Published    = a.Published,
                Index        = a.Index,
                NoResize     = a.NoResize,
                Size         = a.Size,
                ExpiryDate   = a.ExpiryDate,
                Products     = a.Products
                                  .Select(p => new ProductDTO { Id = p.Id, Name = p.Name })
                                  .ToList(),
                CategoryNames = a.AttachmentCategories
                    .Select(cat =>
                        allNameTranslations
                          .Where(t => t.TranslatableId == cat.Id 
                                   && t.LanguageCode   == a.LanguageCode)
                          .Select(t => t.LanguageTranslation)
                          .FirstOrDefault()
                      ?? allNameTranslations
                          .Where(t => t.TranslatableId == cat.Id)
                          .Select(t => t.LanguageTranslation)
                          .FirstOrDefault()
                    )
                    .Where(n => n != null)
                    .ToList()!
            }).ToList();
        }

        public Task<List<Country>> GetAllAttachmentsCountries()
            => _context.Countries.ToListAsync();

        public Task<List<AttachmentCategory>> GetAllAttachmentsCategories()
            => _context.AttachmentCategories.ToListAsync();

        public Task<bool> UpdateAttachmentAsync(AttachmentDTOForById dto)
        {
            var a = _context.Attachments
                .Include(x => x.Products)
                .Include(x => x.Countries)
                .Include(x => x.AttachmentCategories)
                .FirstOrDefault(x => x.Id == dto.Id);
            if (a == null) return Task.FromResult(false);

            // scalars
            a.Name         = dto.Name;
            a.LanguageCode = dto.LanguageCode;
            a.Published    = dto.Published;
            a.Index        = dto.Index;
            a.NoResize     = dto.NoResize;
            a.Size         = dto.Size;
            a.ExpiryDate   = dto.ExpiryDate;
            a.Md5          = dto.Md5;
            a.Content      = dto.Content;

            // sync countries
            a.Countries.Clear();
            foreach (var name in dto.CountryNames)
                if (_context.Countries.FirstOrDefault(c => c.Name == name) is Country c)
                    a.Countries.Add(c);

            // sync categories by translation text
            a.AttachmentCategories.Clear();
            foreach (var catName in dto.CategoryNames)
                if (_context.AttachmentCategories
                    .FirstOrDefault(cat =>
                        _context.Translations.Any(t =>
                            t.TranslatableId       == cat.Id
                         && t.Property             == "Name"
                         && t.LanguageTranslation  == catName))
                   is AttachmentCategory cat)
                    a.AttachmentCategories.Add(cat);

            _context.Attachments.Update(a);
            return _context.SaveChangesAsync()
                           .ContinueWith(t => t.Status == TaskStatus.RanToCompletion);
        }

        public Task<List<Language>> GetAllLanguages()
            => _context.Languages.ToListAsync();
    }
}
