
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
            entity.Property(e => e.Md5).HasColumnName("MD5");
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
