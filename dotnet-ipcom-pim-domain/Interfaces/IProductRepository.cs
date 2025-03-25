using dotnet_ipcom_pim_domain.DTOs.Filters;
using dotnet_ipcom_pim_domain.Entities;

namespace dotnet_ipcom_pim_domain.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetProductByIdAsync(Guid id);
    Task<(List<Product> Products, int TotalCount)> GetProductsAsync(ProductFilterDTO productFilterDto, int page = 1, int pageSize = 10);
}