namespace dotnet_ipcom_pim_domain.Entities;

public class Country
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string CountryCode { get; set; } = null!;
    public DateTime? CreatedOn { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    public virtual ICollection<Language> Languages { get; set; } = new List<Language>();
    public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
    public virtual ICollection<CountryLanguage> CountryLanguages { get; set; } = new List<CountryLanguage>();



}