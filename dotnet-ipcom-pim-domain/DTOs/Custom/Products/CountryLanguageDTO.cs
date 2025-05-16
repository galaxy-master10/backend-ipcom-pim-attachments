namespace dotnet_ipcom_pim_domain.DTOs.Custom;

public class CountryLanguageDTO
{
    public Guid Id { get; set; }
    public string? CountryName { get; set; }
    public string? LanguageName { get; set; }
    public string? LanguageISOCode { get; set; }
}