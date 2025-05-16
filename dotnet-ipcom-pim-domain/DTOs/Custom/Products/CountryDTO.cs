namespace dotnet_ipcom_pim_domain.DTOs.Custom;

public class CountryDTO
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string CountryCode { get; set; } = null!;
}