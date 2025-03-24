using System;
using System.Collections.Generic;

namespace dotnet_ipcom_pim_domain.Entities;

public partial class Product
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public bool? Published { get; set; }

    public int? Index { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? LastModifiedOn { get; set; }

    public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
}
