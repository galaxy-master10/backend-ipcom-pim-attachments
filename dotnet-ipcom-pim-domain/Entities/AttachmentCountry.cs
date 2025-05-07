namespace dotnet_ipcom_pim_domain.Entities;

public class AttachmentCountry
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string CountryCode { get; set; }
    
    public virtual ICollection<Attachment> Attachments { get; set; } 
        = new List<Attachment>();
}