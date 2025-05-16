namespace dotnet_ipcom_pim_domain.Entities;

public class Reference
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public bool? Published { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public int? Index { get; set; }
    
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

}