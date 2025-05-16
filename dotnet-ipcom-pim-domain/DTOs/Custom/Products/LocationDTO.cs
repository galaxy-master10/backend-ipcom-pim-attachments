namespace dotnet_ipcom_pim_domain.DTOs.Custom;

public class LocationDTO
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public double? Longitude { get; set; }
    public double? Latitude { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
}