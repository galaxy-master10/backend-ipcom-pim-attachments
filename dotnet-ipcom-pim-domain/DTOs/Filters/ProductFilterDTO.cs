namespace dotnet_ipcom_pim_domain.DTOs.Filters;

public class ProductFilterDTO
{
    public string? Name { get; set; }
    public bool? Published { get; set; }
    public bool? IsDeleted { get; set; }
    public Guid? AttachmentId { get; set; }
    public int? Index { get; set; }
}