namespace dotnet_ipcom_pim_domain.Entities;

public class CountryLanguage
{
    public Guid Id { get; set; }
    public Guid Countries_Id { get; set; }
    public Guid Languages_Id { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    
     
    public virtual Country Country { get; set; } = null!;
    public virtual Language Language { get; set; } = null!;
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    public virtual ICollection<CompetenceCenter> CompetenceCenters { get; set; } = new List<CompetenceCenter>();

}