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
    public virtual ICollection<Brand> Brands { get; set; } = new List<Brand>();
    public virtual ICollection<CompetenceCenter> CompetenceCenters { get; set; } = new List<CompetenceCenter>();
    public virtual ICollection<Country> Countries { get; set; } = new List<Country>();
    public virtual ICollection<CountryLanguage> CountryLanguages { get; set; } = new List<CountryLanguage>();
    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();
    public virtual ICollection<Reference> References { get; set; } = new List<Reference>();
    public virtual ICollection<ProductCode> ProductCodes { get; set; } = new List<ProductCode>();
    public virtual ICollection<ProductCharacteristic> ProductCharacteristics { get; set; } = new List<ProductCharacteristic>();

    
    public virtual ICollection<Taxonomy1> ProductGroups { get; set; } = new List<Taxonomy1>();
    public virtual ICollection<Taxonomy2> Taxonomy2s { get; set; } = new List<Taxonomy2>();
    public virtual ICollection<Taxonomy3> Taxonomy3s { get; set; } = new List<Taxonomy3>();
    public virtual ICollection<Taxonomy4> Taxonomy4s { get; set; } = new List<Taxonomy4>();
    public virtual ICollection<Taxonomy5> Taxonomy5s { get; set; } = new List<Taxonomy5>();
    public virtual ICollection<Taxonomy6> Taxonomy6s { get; set; } = new List<Taxonomy6>();
 
    
    public virtual ICollection<Translation> Translations { get; set; }
        = new List<Translation>();
    
}
