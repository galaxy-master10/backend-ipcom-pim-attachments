using System.ComponentModel.DataAnnotations;
using dotnet_ipcom_pim_domain.DTOs.Filters;
using System.Text.RegularExpressions;

namespace dotnet_ipcom_pim_application.Validators.Filters;

public class AttachmentFilterValidator
{
    public void Validate(AttachmentFilterDTO dto)
    {
        if (dto == null)
            throw new ValidationException("Filter cannot be null.");
        
        if (dto.Index is < 0)
            throw new ValidationException("Index cannot be negative.");
        
        if (dto.ProductId.HasValue && dto.ProductId == Guid.Empty)
            throw new ValidationException("ProductId cannot be an empty GUID.");
        
        if (!string.IsNullOrWhiteSpace(dto.Name) && dto.Name.Length > 255)
            throw new ValidationException("Attachment name cannot exceed 255 characters.");
        
        if (!string.IsNullOrWhiteSpace(dto.LanguageCode))
        {
            if (dto.LanguageCode.Length > 10)
                throw new ValidationException("Language code cannot exceed 10 characters.");

            if (!Regex.IsMatch(dto.LanguageCode, @"^[a-zA-Z]{2,10}$"))
                throw new ValidationException("Language code must be alphabetic (e.g., 'EN', 'FR').");
        }
        
        if (dto.ExpiryDateFrom.HasValue && dto.ExpiryDateFrom < DateOnly.FromDateTime(DateTime.Today))
            throw new ValidationException("Expiry date from cannot be in the past.");

        if (dto.ExpiryDateTo.HasValue && dto.ExpiryDateFrom.HasValue &&
            dto.ExpiryDateTo < dto.ExpiryDateFrom)
            throw new ValidationException("Expiry date to cannot be earlier than expiry date from.");
        
        if (dto.NoResize == true && dto.Published == false)
            throw new ValidationException("Cannot request 'NoResize=true' on an unpublished attachment.");
    }
}
