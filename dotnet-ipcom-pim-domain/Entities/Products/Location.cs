namespace dotnet_ipcom_pim_domain.Entities;

public class Location
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public byte[]? Logo { get; set; }
    public string? Address { get; set; }
    public double? Longitude { get; set; }
    public double? Latitude { get; set; }
    public string? Email { get; set; }
    public string? CompanyContact { get; set; }
    public string? Website { get; set; }
    public string? NameContact { get; set; }
    public string? PhoneContact { get; set; }
    public string? EmailContact { get; set; }
    public int? Index { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public int? LocationTypeId { get; set; }
    
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();

}