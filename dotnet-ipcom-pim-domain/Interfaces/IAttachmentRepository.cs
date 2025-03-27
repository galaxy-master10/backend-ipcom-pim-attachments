using dotnet_ipcom_pim_domain.DTOs.Filters;
using dotnet_ipcom_pim_domain.Entities;

namespace dotnet_ipcom_pim_domain.Interfaces;

public interface IAttachmentRepository
{
    Task<Attachment?> GetAttachmentByIdAsync(Guid id);
    Task<(List<Attachment> Attachments, int TotalCount, int ExpiringWithin7Days, int ExpiringWithin30Days)> GetAttachmentsAsync(AttachmentFilterDTO attachmentFilterDto, int page = 1, int pageSize = 10);
}