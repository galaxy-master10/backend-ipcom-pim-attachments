using dotnet_ipcom_pim_domain.DTOs.Custom;
using dotnet_ipcom_pim_domain.DTOs.Filters;
using dotnet_ipcom_pim_domain.Entities;
using dotnet_ipcom_pim_share.Common.Utilities;

namespace dotnet_ipcom_pim_application.Interfaces;

public interface IAttachmetService
{
    Task<Attachment> GetAttachmetByIdAsync(Guid id);
  Task<PaginatedResponse<AttachmentSimpleDTO>> GetAttachmentsAsync(AttachmentFilterDTO attachmentFilterDto, int page=1, int pageSize=10);
}