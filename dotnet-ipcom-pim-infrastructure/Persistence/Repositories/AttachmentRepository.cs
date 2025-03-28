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
    
    public async Task<Attachment?> GetAttachmentByIdAsync(Guid id)
    {
        return await _context.Attachments
            .Include(a=>a.Products)
           .FirstOrDefaultAsync(a => a.Id == id);
    }
    public async Task<(List<Attachment> Attachments, int TotalCount, int ExpiringWithin7Days, int ExpiringWithin30Days)> GetAttachmentsAsync(
        AttachmentFilterDTO filter, int page = 1, int pageSize = 10)
    {
        page = page <= 0 ? 1 : page;
        pageSize = pageSize <= 0 ? 10 : pageSize;

        var query = _context.Attachments
            .Include(a => a.Products)
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
            .ToListAsync();

        return (attachments, totalCount, expiringWithin7Days, expiringWithin30Days);
    }
// add a method for getting all attachments at once with no parameter 

    public async Task<List<Attachment>> GetAttachmentsForConsoleAppAsync()
    {
        var query = _context.Attachments
            .Include(a => a.Products)
            .AsQueryable();

        var attachments = await query
            .ToListAsync();

        return attachments;
    }
    
}