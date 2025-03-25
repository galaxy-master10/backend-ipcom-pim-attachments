using dotnet_ipcom_pim_application.Interfaces;
using dotnet_ipcom_pim_application.Validators.Filters;
using dotnet_ipcom_pim_domain.DTOs.Filters;
using dotnet_ipcom_pim_domain.Entities;
using dotnet_ipcom_pim_domain.Interfaces;
using dotnet_ipcom_pim_share.Common.Utilities;

namespace dotnet_ipcom_pim_application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ProductFilterValidator _productFilterValidator;
    
    public ProductService(IProductRepository productRepository, ProductFilterValidator productFilterValidator)
    {
        _productRepository = productRepository;
        _productFilterValidator = productFilterValidator;
    }
    
    public async Task<Product> GetProductByIdAsync(Guid id)
    {
        if (id == null)
        {
            return null;
        }
        
        return await _productRepository.GetProductByIdAsync(id);
    }

    public async Task<PaginatedResponse<Product>> GetProductsAsync(ProductFilterDTO productFilterDto, int page = 1, int pageSize = 10)
    {
        page = Math.Max(1, page);
        pageSize = Math.Max(1, Math.Min(pageSize, 100));
        
        _productFilterValidator.Validate(productFilterDto);
        var (products, totalCount) = await _productRepository.GetProductsAsync(productFilterDto, page, pageSize);
        
        return new PaginatedResponse<Product>(products, totalCount, page, pageSize);
    }
}