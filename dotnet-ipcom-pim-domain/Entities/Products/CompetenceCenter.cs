namespace dotnet_ipcom_pim_domain.Entities;

public class CompetenceCenter
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    public virtual ICollection<CountryLanguage> CountryLanguages { get; set; } = new List<CountryLanguage>();
    
    public virtual ICollection<Taxonomy1> Taxonomy1s { get; set; } = new List<Taxonomy1>();
    public virtual ICollection<Taxonomy2> Taxonomy2s { get; set; } = new List<Taxonomy2>();
    public virtual ICollection<Taxonomy3> Taxonomy3s { get; set; } = new List<Taxonomy3>();
    public virtual ICollection<Taxonomy4> Taxonomy4s { get; set; } = new List<Taxonomy4>();
    public virtual ICollection<Taxonomy5> Taxonomy5s { get; set; } = new List<Taxonomy5>();
    public virtual ICollection<Taxonomy6> Taxonomy6s { get; set; } = new List<Taxonomy6>();


}