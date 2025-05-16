namespace dotnet_ipcom_pim_domain.DTOs.Custom;

public class ProductCodeDTO
{
    public Guid Id { get; set; }
    public string SupplierCode { get; set; } = null!;
    public string EANCode { get; set; } = null!;

}