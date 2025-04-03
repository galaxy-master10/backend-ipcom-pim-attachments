namespace dotnet_ipcom_pim_domain.Entities;

public partial class AttachmentCategory
{
    public Guid Id { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    
    public virtual ICollection<Attachment> Attachments { get; set; } 
        = new List<Attachment>();

    public virtual ICollection<Translation> Translations { get; set; }
        = new List<Translation>();
}