namespace dotnet_ipcom_pim_domain.Entities;

public class Language
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ISOCode { get; set; }
    
    public virtual ICollection<CountryLanguage> CountryLanguages { get; set; } = new List<CountryLanguage>();
    public virtual ICollection<Country> Countries { get; set; } = new List<Country>();

}