namespace dotnet_ipcom_pim_domain.Entities;

public class CompetenceCenter
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    public virtual ICollection<CountryLanguage> CountryLanguages { get; set; } = new List<CountryLanguage>();

}