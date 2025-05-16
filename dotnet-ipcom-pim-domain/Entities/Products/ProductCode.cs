namespace dotnet_ipcom_pim_domain.Entities;

public class ProductCode
{
    public Guid Id { get; set; }
    public string SupplierCode { get; set; } = null!;
    public string EANCode { get; set; } = null!;
    public Guid Product_Id { get; set; }
    
    public virtual Product Product { get; set; } = null!;
}