namespace dotnet_ipcom_pim_domain.Entities;

public partial class Translation
{
    public Guid Id { get; set; }

    // e.g. "EN", "NL", etc. from [LanguageCodes] table
    public string LanguageCode { get; set; } = null!;

    // The actual translated text, e.g. "Drawings" or "Manuals"
    public string LanguageTranslation { get; set; } = null!;

    // Which property is this a translation of? e.g. "Name"
    public string Property { get; set; } = null!;

    // The entity (AttachmentCategory, Brand, Taxonomy, etc.) to which this text belongs
    public Guid TranslatableId { get; set; }

    // Optional navigation back to the category
    // (We will configure a relationship so that if Property='Name' 
    //  and TranslatableId=AttachmentCategory.Id, we can find it.)
    public virtual AttachmentCategory? AttachmentCategory { get; set; }
}