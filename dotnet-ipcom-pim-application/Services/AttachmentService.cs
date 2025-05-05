using dotnet_ipcom_pim_application.Interfaces;
using dotnet_ipcom_pim_application.Validators.Filters;
using dotnet_ipcom_pim_domain.DTOs.Custom;
using dotnet_ipcom_pim_domain.DTOs.Filters;
using dotnet_ipcom_pim_domain.Entities;
using dotnet_ipcom_pim_domain.Interfaces;
using dotnet_ipcom_pim_share.Common.Utilities;

namespace dotnet_ipcom_pim_application.Services;

public class AttachmentService : IAttachmetService
{
    private readonly IAttachmentRepository _attachmentRepository;
    private readonly AttachmentFilterValidator _attachmentFilterValidator;
    
    public AttachmentService(IAttachmentRepository attachmentRepository, AttachmentFilterValidator attachmentFilterValidator)
    {
        _attachmentRepository = attachmentRepository;
        _attachmentFilterValidator = attachmentFilterValidator;
    }
    
    public async Task<AttachmentDTOForById> GetAttachmetByIdAsync(Guid id)
    {
        if (id == null)
        {
            return null;
        }
        
        return await _attachmentRepository.GetAttachmentByIdAsync(id);
    }

    public async Task<PaginatedResponse<AttachmentDTO>> GetAttachmentsAsync(AttachmentFilterDTO attachmentFilterDto, int page = 1, int pageSize = 10)
    {
        page = Math.Max(1, page);
        pageSize = Math.Max(1, Math.Min(pageSize, 100));
        
        _attachmentFilterValidator.Validate(attachmentFilterDto);
        var (attachments, totalCount, expiringWithin7Days, expiringWithin30Days) = 
            await _attachmentRepository.GetAttachmentsAsync(attachmentFilterDto, page, pageSize);

        return new PaginatedResponse<AttachmentDTO>(
            attachments, 
            page, 
            pageSize, 
            totalCount, 
            expiringWithin7Days, 
            expiringWithin30Days);    }
}