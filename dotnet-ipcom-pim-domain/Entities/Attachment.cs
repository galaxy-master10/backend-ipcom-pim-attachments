using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_ipcom_pim_domain.Entities;

public partial class Attachment
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
    
    public string LanguageCode { get; set; } = null!;

    public bool? Published { get; set; }

    public int? Index { get; set; }

    public bool NoResize { get; set; }

    public long Size { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    
    
    [NotMapped]
    public List<string> CategoryNames { get; set; } = new List<string>();
    public virtual ICollection<AttachmentCategory> AttachmentCategories { get; set; }
        = new List<AttachmentCategory>();
    
    public byte[]? Md5 { get; set; } // New column

    public byte[]? Content { get; set; } // New column
}
