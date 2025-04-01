namespace dotnet_ipcom_pim_domain.DTOs.Custom;

public class AttachmentWithCategoryDTO
{
    public Guid AttachmentId { get; set; }
    public string AttachmentName { get; set; } = null!;
    public List<CategoryDTO> Categories { get; set; } = new();
}
