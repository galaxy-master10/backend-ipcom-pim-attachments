using dotnet_ipcom_pim_domain.DTOs.Custom;
using dotnet_ipcom_pim_domain.DTOs.Filters;
using dotnet_ipcom_pim_domain.Entities;

namespace dotnet_ipcom_pim_domain.Interfaces;

public interface IAttachmentRepository
{
    Task<AttachmentDTOForById?> GetAttachmentByIdAsync(Guid id);
    Task<(List<AttachmentDTO> Attachments, int TotalCount, int ExpiringWithin7Days, int ExpiringWithin30Days)> GetAttachmentsAsync(AttachmentFilterDTO attachmentFilterDto, int page = 1, int pageSize = 10);
    Task<List<AttachmentDTO>> GetAttachmentsForConsoleAppAsync();
    Task<List<AttachmentCountry>> GetAllAttachmentsCountries();
    Task<List<AttachmentCategory>> GetAllAttachmentsCategories();
    
}