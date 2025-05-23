namespace dotnet_ipcom_pim_domain.Entities;

public partial class Translation
{
    public Guid Id { get; set; }
    public string LanguageCode { get; set; } = null!;
    
    public string LanguageTranslation { get; set; } = null!;
    
    public string Property { get; set; } = null!;
    
    public Guid TranslatableId { get; set; }
    
}