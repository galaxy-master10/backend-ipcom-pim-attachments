using dotnet_ipcom_pim_domain.DTOs.Custom;
using dotnet_ipcom_pim_domain.DTOs.Filters;
using dotnet_ipcom_pim_domain.Entities;
using dotnet_ipcom_pim_share.Common.Utilities;

namespace dotnet_ipcom_pim_application.Interfaces;

public interface IAttachmetService
{
    Task<AttachmentDTOForById> GetAttachmetByIdAsync(Guid id);
  Task<PaginatedResponse<AttachmentDTO>> GetAttachmentsAsync(AttachmentFilterDTO attachmentFilterDto, int page=1, int pageSize=10);
  Task<List<Country>> GetAllAttachmentsCountries();
    Task<List<AttachmentCategory>> GetAllAttachmentsCategories();
    // update attachment
    Task<bool> UpdateAttachmentAsync(AttachmentDTOForById attachmentDto);
    
    Task<List<Language>> GetAllLanguages();
}