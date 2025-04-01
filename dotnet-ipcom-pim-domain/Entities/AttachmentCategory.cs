namespace dotnet_ipcom_pim_domain.Entities;

public partial class AttachmentCategory
{
    public Guid Id { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? LastModifiedOn { get; set; }

    // Navigation: which Attachments are in this Category?
    public virtual ICollection<Attachment> Attachments { get; set; } 
        = new List<Attachment>();

    // Navigation: translations for e.g. “Name”
    public virtual ICollection<Translation> Translations { get; set; }
        = new List<Translation>();
}