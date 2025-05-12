namespace dotnet_ipcom_pim_domain.DTOs.Custom;

public class AttachmentDTOForById
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string LanguageCode { get; set; } = null!;
    public bool? Published { get; set; }
    public int? Index { get; set; }
    public bool NoResize { get; set; }
    public long Size { get; set; }
    public DateOnly? ExpiryDate { get; set; }
    public List<ProductDTO> Products { get; set; } = new();
    public List<string> CategoryNames { get; set; } = new();
    
    public byte[]? Md5 { get; set; }

    public byte[]? Content { get; set; } 
    public List<string> CountryNames { get; set; } = new();
}