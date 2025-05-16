namespace dotnet_ipcom_pim_domain.Entities;

public class Brand
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

}