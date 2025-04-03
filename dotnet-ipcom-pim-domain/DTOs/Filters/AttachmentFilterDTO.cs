namespace dotnet_ipcom_pim_domain.DTOs.Filters;

public class AttachmentFilterDTO
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public string? LanguageCode { get; set; }
    public bool? Published { get; set; }
    public bool? NoResize { get; set; }
    
    public DateOnly? ExpiryDateFrom { get; set; }
    public DateOnly? ExpiryDateTo { get; set; }

    public Guid? ProductId { get; set; }
    public int? Index { get; set; }
    
    public string? CategoryName { get; set; }
    
}