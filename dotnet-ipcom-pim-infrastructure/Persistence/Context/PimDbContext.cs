
using dotnet_ipcom_pim_domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace dotnet_ipcom_pim_infrastructure.Persistence.Context;

public partial class PimDbContext : DbContext
{
    public PimDbContext(DbContextOptions<PimDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attachment> Attachments { get; set; }

    public virtual DbSet<Product> Products { get; set; }
    
    public virtual DbSet<AttachmentCategory> AttachmentCategories { get; set; }
   // public virtual DbSet<AttachmentCountry> AttachmentCountries { get; set; }
    
    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }
    public virtual DbSet<CompetenceCenter> CompetenceCenters { get; set; }
    public virtual DbSet<CountryLanguage> CountryLanguages { get; set; }
    public virtual DbSet<Location> Locations { get; set; }
    public virtual DbSet<Reference> References { get; set; }
    public virtual DbSet<ProductCode> ProductCodes { get; set; }
    public virtual DbSet<ProductCharacteristic> ProductCharacteristics { get; set; }
    public virtual DbSet<Taxonomy1> Taxonomy1 { get; set; }
    public virtual DbSet<Taxonomy2> Taxonomy2 { get; set; }
    public virtual DbSet<Taxonomy3> Taxonomy3 { get; set; }
    public virtual DbSet<Taxonomy4> Taxonomy4 { get; set; }
    public virtual DbSet<Taxonomy5> Taxonomy5 { get; set; }
    public virtual DbSet<Taxonomy6> Taxonomy6 { get; set; }
    public virtual DbSet<Country> Countries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_CI_AS");

        modelBuilder.Entity<Attachment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Attachme__3214EC07E4754C59");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.LanguageCode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
         
         entity
             .HasMany(a => a.AttachmentCategories)
             .WithMany(c => c.Attachments)
             .UsingEntity<Dictionary<string, object>>(
                 "AttachmentsXAttachmentCategories",
                 r => r
                     .HasOne<AttachmentCategory>()
                     .WithMany()
                     .HasForeignKey("AttachmentCategory_Id")
                     .HasConstraintName("FK_AttachmentsXAttachmentCategories_AttachmentCategory")
                     .OnDelete(DeleteBehavior.Cascade),
                 l => l
                     .HasOne<Attachment>()
                     .WithMany()
                     .HasForeignKey("Attachment_Id")
                     .HasConstraintName("FK_AttachmentsXAttachmentCategories_Attachment"),
                 j =>
                 {
                     j.HasKey("Attachment_Id", "AttachmentCategory_Id");
                     j.ToTable("AttachmentsXAttachmentCategories");
                 }
             );
         
         entity
             .HasMany(a => a.Countries)
             .WithMany(c => c.Attachments)
              .UsingEntity<Dictionary<string, object>>(
                    "AttachmentsXCountries",
                    r => r
                        .HasOne<Country>() // Changed from AttachmentCountry
                        .WithMany()
                        .HasForeignKey("Country_Id")
                        .HasConstraintName("FK_AttachmentsXCountries_Country")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                 l => l
                     .HasOne<Attachment>()
                     .WithMany()
                     .HasForeignKey("Attachment_Id")
                     .HasConstraintName("FK_AttachmentsXCountries_Attachment")
                     .OnDelete(DeleteBehavior.ClientSetNull),
                 j =>
                 {
                     j.HasKey("Attachment_Id", "Country_Id");
                     j.ToTable("AttachmentsXCountries");
                 }
             );
         
        });

       
        modelBuilder.Entity<AttachmentCategory>(entity =>
        {
            entity.ToTable("AttachmentCategories");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();

            
            entity
                .HasMany(ac => ac.Translations)
                .WithOne(t => t.AttachmentCategory!)
                .HasForeignKey(t => t.TranslatableId)
                .HasConstraintName("FK_Translations_AttachmentCategories")
                .OnDelete(DeleteBehavior.ClientSetNull);
            
        });
      
        modelBuilder.Entity<Translation>(entity =>
        {
            entity.ToTable("Translations");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.LanguageCode)
                .HasMaxLength(2)
                .IsFixedLength();
            entity.Property(e => e.Property)
                .HasMaxLength(50);

        });
        
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC07AFDC2938");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(1024);

            entity.HasMany(d => d.Attachments).WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductsXattachment",
                    r => r.HasOne<Attachment>().WithMany()
                        .HasForeignKey("AttachmentId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ProductsX__Attac__02C769E9"),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__ProductsX__Produ__01D345B0"),
                    j =>
                    {
                        j.HasKey("ProductId", "AttachmentId").HasName("PK__Products__F14AB0553E6C9A2C");
                        j.ToTable("ProductsXAttachments");
                        j.IndexerProperty<Guid>("ProductId").HasColumnName("Product_Id");
                        j.IndexerProperty<Guid>("AttachmentId").HasColumnName("Attachment_Id");
                    });
            
            // Configure many-to-many relationship with Brands
            entity.HasMany(d => d.Brands).WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductsXBrands",
                    r => r.HasOne<Brand>().WithMany()
                        .HasForeignKey("Brand_Id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("Product_Id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("Brand_Id", "Product_Id");
                        j.ToTable("ProductsXBrands");
                    });
            
            // Configure many-to-many relationship with CompetenceCenters
            entity.HasMany(d => d.CompetenceCenters).WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductsXCompetenceCenters",
                    r => r.HasOne<CompetenceCenter>().WithMany()
                        .HasForeignKey("CompetenceCenter_Id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("Product_Id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("CompetenceCenter_Id", "Product_Id");
                        j.ToTable("ProductsXCompetenceCenters");
                    });
            
            // Configure many-to-many relationship with Countries
            entity.HasMany(d => d.Countries).WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductsXCountries",
                    r => r.HasOne<Country>().WithMany()
                        .HasForeignKey("Country_Id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("Product_Id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("Country_Id", "Product_Id");
                        j.ToTable("ProductsXCountries");
                    });
            
            // Configure many-to-many relationship with CountryLanguages
            entity.HasMany(d => d.CountryLanguages).WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductsXCountryLanguages",
                    r => r.HasOne<CountryLanguage>().WithMany()
                        .HasForeignKey("CountryLanguage_Id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("Product_Id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("CountryLanguage_Id", "Product_Id");
                        j.ToTable("ProductsXCountryLanguages");
                    });
            
            // Configure many-to-many relationship with Locations
            entity.HasMany(d => d.Locations).WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductsXLocations",
                    r => r.HasOne<Location>().WithMany()
                        .HasForeignKey("Location_Id") // Explicitly use underscored name
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("Product_Id") // Explicitly use underscored name
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("Location_Id", "Product_Id");
                        j.ToTable("ProductsXLocations");
            
                        // Explicitly configure column names
                        j.IndexerProperty<Guid>("Location_Id").HasColumnName("Location_Id");
                        j.IndexerProperty<Guid>("Product_Id").HasColumnName("Product_Id");
                    });
            
            // Configure many-to-many relationship with References
            entity.HasMany(d => d.References).WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductsXReferences",
                    r => r.HasOne<Reference>().WithMany()
                        .HasForeignKey("Reference_Id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("Product_Id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("Product_Id", "Reference_Id");
                        j.ToTable("ProductsXReferences");
                    });
            
            
            // Configure many-to-many relationship with Taxonomy1 (ProductGroups)
            entity.HasMany(d => d.ProductGroups).WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductsXProductGroups",
                    r => r.HasOne<Taxonomy1>().WithMany()
                        .HasForeignKey("Taxonomy1_Id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("Product_Id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("Product_Id", "Taxonomy1_Id");
                        j.ToTable("ProductsXProductGroups");
                    });
            
            // Configure many-to-many relationship with Taxonomy2
            entity.HasMany(d => d.Taxonomy2s).WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductsXTaxonomy2",
                    r => r.HasOne<Taxonomy2>().WithMany()
                        .HasForeignKey("Taxonomy2_Id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("Product_Id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("Product_Id", "Taxonomy2_Id");
                        j.ToTable("ProductsXTaxonomy2");
                    });
            
            // Configure many-to-many relationship with Taxonomy3
            entity.HasMany(d => d.Taxonomy3s).WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductsXTaxonomy3",
                    r => r.HasOne<Taxonomy3>().WithMany()
                        .HasForeignKey("Taxonomy3_Id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("Product_Id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("Product_Id", "Taxonomy3_Id");
                        j.ToTable("ProductsXTaxonomy3");
                    });
            
            // Configure many-to-many relationship with Taxonomy4
            entity.HasMany(d => d.Taxonomy4s).WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductsXTaxonomy4",
                    r => r.HasOne<Taxonomy4>().WithMany()
                        .HasForeignKey("Taxonomy4_Id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("Product_Id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("Product_Id", "Taxonomy4_Id");
                        j.ToTable("ProductsXTaxonomy4");
                    });
            
            // Configure many-to-many relationship with Taxonomy5
            entity.HasMany(d => d.Taxonomy5s).WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductsXTaxonomy5",
                    r => r.HasOne<Taxonomy5>().WithMany()
                        .HasForeignKey("Taxonomy5_Id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("Product_Id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("Product_Id", "Taxonomy5_Id");
                        j.ToTable("ProductsXTaxonomy5");
                    });
            
            // Configure many-to-many relationship with Taxonomy6
            entity.HasMany(d => d.Taxonomy6s).WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductsXTaxonomy6",
                    r => r.HasOne<Taxonomy6>().WithMany()
                        .HasForeignKey("Taxonomy6_Id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("Product_Id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("Product_Id", "Taxonomy6_Id");
                        j.ToTable("ProductsXTaxonomy6");
                    });
            
            // Configure one-to-many relationship with ProductCodes
            entity.HasMany(e => e.ProductCodes)
                .WithOne(e => e.Product)
                .HasForeignKey(e => e.Product_Id)
                .OnDelete(DeleteBehavior.ClientSetNull);

            // Configure one-to-many relationship with ProductCharacteristics
            entity.HasMany(e => e.ProductCharacteristics)
                .WithOne(e => e.Product)
                .HasForeignKey(e => e.Product_Id)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });
        // Configure ProductCode entity
        modelBuilder.Entity<ProductCode>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.SupplierCode).HasMaxLength(128).IsRequired();
            entity.Property(e => e.EANCode).HasMaxLength(128).IsRequired();
            entity.Property(e => e.Product_Id).IsRequired();
        });
        
        // Configure ProductCharacteristic entity
        modelBuilder.Entity<ProductCharacteristic>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Width).HasMaxLength(128);
            entity.Property(e => e.Length).HasMaxLength(128);
            entity.Property(e => e.Thickness).HasMaxLength(128);
            entity.Property(e => e.Diameter).HasMaxLength(128);
            entity.Property(e => e.Lambda).HasMaxLength(128);
            entity.Property(e => e.R).HasMaxLength(128);
            entity.Property(e => e.FireClass).HasMaxLength(128);
            entity.Property(e => e.EdgeFinish).HasMaxLength(128);
            entity.Property(e => e.PressureStrength).HasMaxLength(128);
            entity.Property(e => e.Coating).HasMaxLength(128);
            entity.Property(e => e.Density).HasMaxLength(128);
            entity.Property(e => e.Volume).HasMaxLength(128);
            entity.Property(e => e.Weight).HasMaxLength(128);
        });
        
        // Configure Brand entity
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");
        });

        // Configure CompetenceCenter entity
        modelBuilder.Entity<CompetenceCenter>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");
        });
        
        // Configure CountryLanguage entity
        modelBuilder.Entity<CountryLanguage>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");
    
            // Fix: Navigation property name must match the property in the entity class
            entity.HasOne(d => d.Country)
                .WithMany(p => p.CountryLanguages)
                .HasForeignKey(d => d.Countries_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CountryLanguages_Countries");

            entity.HasOne(d => d.Language)
                .WithMany(p => p.CountryLanguages)
                .HasForeignKey(d => d.Languages_Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CountryLanguages_Languages");
        
            // Specify the actual table name
            entity.ToTable("CountryLanguages");
        });
        
        // Configure Location entity
        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");
        });
        
        // Configure Reference entity
        modelBuilder.Entity<Reference>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");
        });
        
        // Configure Taxonomy1 entity
        modelBuilder.Entity<Taxonomy1>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");
        });
        
        // Configure Taxonomy2 entity
        modelBuilder.Entity<Taxonomy2>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");
        });
        
        // Configure Taxonomy3 entity
        modelBuilder.Entity<Taxonomy3>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");
        });
        
        // Configure Taxonomy4 entity
        modelBuilder.Entity<Taxonomy4>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");
        });
        
        // Configure Taxonomy5 entity
        modelBuilder.Entity<Taxonomy5>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");
        });
        
        // Configure Taxonomy6 entity
        modelBuilder.Entity<Taxonomy6>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");
        });
        
  

        // Keep or update the Country configuration
        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("Countries");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.CountryCode)
                .HasMaxLength(2)
                .IsFixedLength();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");
        });
        
        modelBuilder.Entity<Language>(entity =>
        {
            entity.ToTable("Languages");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.ISOCode)
                .HasMaxLength(2)
                .IsFixedLength();
        });
        
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}


