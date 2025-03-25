using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using dotnet_ipcom_pim_domain.DTOs.Filters;

namespace dotnet_ipcom_pim_application.Validators.Filters;

public class ProductFilterValidator
{
    public void Validate(ProductFilterDTO productFilterDto)
    {
        if (productFilterDto == null)
            throw new ValidationException("Filter cannot be null.");

        if (productFilterDto.Index is < 0)
            throw new ValidationException("Index cannot be negative.");

        if (productFilterDto.AttachmentId.HasValue && productFilterDto.AttachmentId == Guid.Empty)
            throw new ValidationException("AttachmentId cannot be an empty GUID.");

        if (!string.IsNullOrWhiteSpace(productFilterDto.Name) && productFilterDto.Name.Length > 255)
            throw new ValidationException("Name cannot exceed 255 characters.");

        if (productFilterDto.Published.HasValue && productFilterDto.Published == false)
            throw new ValidationException("Published cannot be false.");
        
        if (productFilterDto.IsDeleted.HasValue && productFilterDto.IsDeleted == false)
            throw new ValidationException("IsDeleted cannot be false.");

    }



}