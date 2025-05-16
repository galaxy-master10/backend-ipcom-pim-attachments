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
       /*
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id);
    
        if (product == null)
            return null;

        // Now load related entities using explicit loading
        // Load immediate relationships
        await _context.Entry(product)
            .Collection(p => p.Attachments)
            .LoadAsync();
        
        await _context.Entry(product)
            .Collection(p => p.Brands)
            .LoadAsync();
        
        await _context.Entry(product)
            .Collection(p => p.ProductCodes)
            .LoadAsync();
        
        await _context.Entry(product)
            .Collection(p => p.ProductCharacteristics)
            .LoadAsync();
    
        // Optionally load more relationships if needed
        await _context.Entry(product)
            .Collection(p => p.CompetenceCenters)
            .LoadAsync();
        
        await _context.Entry(product)
            .Collection(p => p.Countries)
            .LoadAsync();
    
        return product;
        */
      
        return await _context.Products
            .Include(p => p.Attachments)
            .Include(p => p.Brands)
            .Include(p => p.CompetenceCenters)
            .Include(p => p.Countries)
            .Include(p => p.CountryLanguages)
            .ThenInclude(cl => cl.Country)
            .Include(p => p.CountryLanguages)
            .ThenInclude(cl => cl.Language)
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
            .FirstOrDefaultAsync(p => p.Id == id);
     
      
   //   return await _context.Products.FindAsync(id);
        
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