namespace dotnet_ipcom_pim_domain.DTOs.Custom;

public class ProductDTO
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public bool? Published { get; set; }
    public int? Index { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    
    // Associated data
    public ICollection<AttachmentDTO>? Attachments { get; set; }
    public ICollection<BrandDTO>? Brands { get; set; }
    public ICollection<CompetenceCenterDTO>? CompetenceCenters { get; set; }
    public ICollection<CountryDTO>? Countries { get; set; }
    public ICollection<CountryLanguageDTO>? CountryLanguages { get; set; }
    public ICollection<LocationDTO>? Locations { get; set; }
    public ICollection<ReferenceDTO>? References { get; set; }
    public ICollection<ProductCodeDTO>? ProductCodes { get; set; }
    public ICollection<ProductCharacteristicDTO>? ProductCharacteristics { get; set; }
    
    // Taxonomies
    public ICollection<TaxonomyDTO>? ProductGroups { get; set; }
    public ICollection<TaxonomyDTO>? ApplicationAreas { get; set; } // Taxonomy2
    public ICollection<TaxonomyDTO>? ApplicationProperties { get; set; } // Taxonomy3
    public ICollection<TaxonomyDTO>? ConstructionTypes { get; set; } // Taxonomy4
    public ICollection<TaxonomyDTO>? ConstructionMaterials { get; set; } // Taxonomy5
    public ICollection<TaxonomyDTO>? ConstructionProperties { get; set; } // Taxonomy6
}

