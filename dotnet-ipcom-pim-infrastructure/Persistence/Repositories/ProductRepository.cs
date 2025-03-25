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
        return await _context.Products
            .Include(p => p.Attachments)
            .FirstOrDefaultAsync(p => p.Id == id);
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