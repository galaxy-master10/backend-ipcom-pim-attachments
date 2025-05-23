using dotnet_ipcom_pim_domain.DTOs.Filters;
using dotnet_ipcom_pim_domain.Entities;
using dotnet_ipcom_pim_domain.Interfaces;
using dotnet_ipcom_pim_infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace dotnet_ipcom_pim_infrastructure.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly PimDbContext _context;
    
    public ProductRepository(PimDbContext context)
    {
        _context = context;
    }
    
    public async Task<Product?> GetProductByIdAsync(Guid id)
    {
     
      
      // 1) Load product + all related entities
    var product = await _context.Products
        .Include(p => p.Attachments)
        .Include(p => p.Brands)
        .Include(p => p.CompetenceCenters)
        .Include(p => p.Countries)
        .Include(p => p.CountryLanguages).ThenInclude(cl => cl.Country)
        .Include(p => p.CountryLanguages).ThenInclude(cl => cl.Language)
        .Include(p => p.Locations)
        .Include(p => p.References)
        .Include(p => p.ProductCodes)
        .Include(p => p.ProductCharacteristics)
        .Include(p => p.ProductGroups)
        .Include(p => p.Taxonomy2s)
        .Include(p => p.Taxonomy3s)
        .Include(p => p.Taxonomy4s)
        .Include(p => p.Taxonomy5s)
        .Include(p => p.Taxonomy6s)
        .AsSplitQuery()
        .AsNoTracking()
        .FirstOrDefaultAsync(p => p.Id == id);

    if (product == null)
        return null;

    // ⬅️ ADDITION: gather all the IDs that could have translations
    var allTranslatableIds = new List<Guid> { product.Id };
    //allTranslatableIds.AddRange(product.Attachments.Select(a => a.Id));
    allTranslatableIds.AddRange(product.Brands.Select(b => b.Id));
    allTranslatableIds.AddRange(product.CompetenceCenters.Select(cc => cc.Id)); 
    allTranslatableIds.AddRange(product.Countries.Select(c => c.Id));
    allTranslatableIds.AddRange(product.Locations.Select(l => l.Id));
    allTranslatableIds.AddRange(product.References.Select(r => r.Id));
    allTranslatableIds.AddRange(product.ProductCodes.Select(pc => pc.Id));
    allTranslatableIds.AddRange(product.ProductCharacteristics.Select(ch => ch.Id));
    allTranslatableIds.AddRange(product.ProductGroups.Select(t1 => t1.Id));
    allTranslatableIds.AddRange(product.Taxonomy2s.Select(t2 => t2.Id));
    allTranslatableIds.AddRange(product.Taxonomy3s.Select(t3 => t3.Id));
    allTranslatableIds.AddRange(product.Taxonomy4s.Select(t4 => t4.Id));
    allTranslatableIds.AddRange(product.Taxonomy5s.Select(t5 => t5.Id));
    allTranslatableIds.AddRange(product.Taxonomy6s.Select(t6 => t6.Id));

    
// Fix the translation query by being explicit about the columns
        var productTranslations = await _context.Translations
            .AsNoTracking()
            .Where(t => t.TranslatableId == product.Id)
            .Select(t => new Translation {
                Id = t.Id,
                LanguageCode = t.LanguageCode,
                LanguageTranslation = t.LanguageTranslation,
                Property = t.Property,
                TranslatableId = t.TranslatableId
            })
            .ToListAsync();

// attach
    product.Translations = productTranslations;
    

    return product;
     
    }
    
    public async Task<(List<Product> Products, int TotalCount)> GetProductsAsync(
        ProductFilterDTO filter, int page = 1, int pageSize = 10)
    {
        page = page <= 0 ? 1 : page;
        pageSize = pageSize <= 0 ? 10 : pageSize;

        var query = _context.Products
            .Include(p => p.Attachments)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Name))
        {
            query = query.Where(p => p.Name.Contains(filter.Name));
        }
        
        if (filter.Published.HasValue)
        {
            query = query.Where(p => p.Published == filter.Published);
        }
        
        if (filter.IsDeleted.HasValue)
        {
            query = query.Where(p => p.IsDeleted == filter.IsDeleted);
        }
        
        if (filter.AttachmentId.HasValue)
        {
            query = query.Where(p => p.Attachments.Any(a => a.Id == filter.AttachmentId));
        }
        
        if (filter.Index.HasValue)
        {
            query = query.Where(p => p.Index == filter.Index);
        }

        var totalCount = await query.CountAsync();
        var products = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (products, totalCount);
    }
    
    
}