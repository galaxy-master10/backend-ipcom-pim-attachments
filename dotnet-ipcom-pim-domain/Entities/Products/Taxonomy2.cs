namespace dotnet_ipcom_pim_domain.Entities;

public class Taxonomy2
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public int? Index { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    public virtual ICollection<CompetenceCenter> CompetenceCenters { get; set; } = new List<CompetenceCenter>();

}