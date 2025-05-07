
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
    public virtual DbSet<AttachmentCountry> AttachmentCountries { get; set; }



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
                     .HasOne<AttachmentCountry>()
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

            // Relationship to Translations
            // We will rely on a condition that "TranslatableId" matches the category's Id
            // and possibly also "Property = 'Name'". 
            // Because your schema is used for many translatable entities,
            // we can do a filtered Include or a condition in queries.

            // If you want a direct relationship, you can do:
            entity
                .HasMany(ac => ac.Translations)
                .WithOne(t => t.AttachmentCategory!)
                .HasForeignKey(t => t.TranslatableId)
                .HasConstraintName("FK_Translations_AttachmentCategories")
                .OnDelete(DeleteBehavior.ClientSetNull);

            // (We typically also store which 'Property' the translation is for.)
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
        });

        modelBuilder.Entity<AttachmentCountry>(entity =>
        {
            entity.ToTable("Countries");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.CountryCode)
                .HasMaxLength(2)
                .IsFixedLength();
        });
        
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}


