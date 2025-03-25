using dotnet_ipcom_pim_domain.DTOs.Filters;
using dotnet_ipcom_pim_domain.Entities;
using dotnet_ipcom_pim_share.Common.Utilities;

namespace dotnet_ipcom_pim_application.Interfaces;

public interface IProductService
{
    Task<Product> GetProductByIdAsync(Guid id);
    Task<PaginatedResponse<Product>> GetProductsAsync(ProductFilterDTO productFilterDto, int page = 1, int pageSize = 10);
    
}