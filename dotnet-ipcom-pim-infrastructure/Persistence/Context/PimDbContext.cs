﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using dotnet_ipcom_pim_domain.Entities;

namespace dotnet_ipcom_pim_infrastructure.Persistence.Context
{
    public partial class PimDbContext : DbContext
    {
        public PimDbContext(DbContextOptions<PimDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<Attachment> Attachments { get; set; } = null!;
        public DbSet<AttachmentCategory> AttachmentCategories { get; set; } = null!;
        public DbSet<Language> Languages { get; set; } = null!;
        public DbSet<Brand> Brands { get; set; } = null!;
        public DbSet<CompetenceCenter> CompetenceCenters { get; set; } = null!;
        public DbSet<CountryLanguage> CountryLanguages { get; set; } = null!;
        public DbSet<Location> Locations { get; set; } = null!;
        public DbSet<Reference> References { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<ProductCode> ProductCodes { get; set; } = null!;
        public DbSet<ProductCharacteristic> ProductCharacteristics { get; set; } = null!;
        public DbSet<Taxonomy1> Taxonomy1 { get; set; } = null!;
        public DbSet<Taxonomy2> Taxonomy2 { get; set; } = null!;
        public DbSet<Taxonomy3> Taxonomy3 { get; set; } = null!;
        public DbSet<Taxonomy4> Taxonomy4 { get; set; } = null!;
        public DbSet<Taxonomy5> Taxonomy5 { get; set; } = null!;
        public DbSet<Taxonomy6> Taxonomy6 { get; set; } = null!;
        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<Translation> Translations { get; set; } = null!;  


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Latin1_General_CI_AS");
            
            //
            // Attachments ↔ AttachmentCategories
            //
            modelBuilder.Entity<Attachment>()
                .HasMany(a => a.AttachmentCategories).WithMany(c => c.Attachments)
                .UsingEntity<Dictionary<string, object>>(
                    "AttachmentsXAttachmentCategories",
                    right => right.HasOne<AttachmentCategory>().WithMany()
                                  .HasForeignKey("AttachmentCategory_Id")
                                  .OnDelete(DeleteBehavior.Cascade),
                    left  => left .HasOne<Attachment>().WithMany()
                                  .HasForeignKey("Attachment_Id")
                                  .OnDelete(DeleteBehavior.Cascade),
                    join  =>
                    {
                        join.ToTable("AttachmentsXAttachmentCategories");
                        join.HasKey("Attachment_Id", "AttachmentCategory_Id");
                    });

            //
            // Attachments ↔ Countries
            //
            modelBuilder.Entity<Attachment>()
                .HasMany(a => a.Countries).WithMany(c => c.Attachments)
                .UsingEntity<Dictionary<string, object>>(
                    "AttachmentsXCountries",
                    right => right.HasOne<Country>().WithMany()
                                  .HasForeignKey("Country_Id")
                                  .OnDelete(DeleteBehavior.ClientSetNull),
                    left  => left .HasOne<Attachment>().WithMany()
                                  .HasForeignKey("Attachment_Id")
                                  .OnDelete(DeleteBehavior.ClientSetNull),
                    join  =>
                    {
                        join.ToTable("AttachmentsXCountries");
                        join.HasKey("Attachment_Id", "Country_Id");
                    });

            //
            // Products ↔ Attachments
            //
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Attachments).WithMany(a => a.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductsXAttachments",
                    right => right.HasOne<Attachment>().WithMany()
                                  .HasForeignKey("Attachment_Id")
                                  .OnDelete(DeleteBehavior.ClientSetNull),
                    left  => left .HasOne<Product>().WithMany()
                                  .HasForeignKey("Product_Id")
                                  .OnDelete(DeleteBehavior.ClientSetNull),
                    join  =>
                    {
                        join.ToTable("ProductsXAttachments");
                        join.HasKey("Product_Id", "Attachment_Id");
                    });

            //
            // Products ↔ Brands
            //
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Brands).WithMany(b => b.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductsXBrands",
                    right => right.HasOne<Brand>().WithMany()
                                  .HasForeignKey("Brand_Id")
                                  .OnDelete(DeleteBehavior.ClientSetNull),
                    left  => left .HasOne<Product>().WithMany()
                                  .HasForeignKey("Product_Id")
                                  .OnDelete(DeleteBehavior.ClientSetNull),
                    join  =>
                    {
                        join.ToTable("ProductsXBrands");
                        join.HasKey("Brand_Id", "Product_Id");
                    });

            //
            // Products ↔ CompetenceCenters
            //
            modelBuilder.Entity<Product>()
                .HasMany(p => p.CompetenceCenters).WithMany(cc => cc.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductsXCompetenceCenters",
                    right => right.HasOne<CompetenceCenter>().WithMany()
                                  .HasForeignKey("CompetenceCenter_Id")
                                  .OnDelete(DeleteBehavior.ClientSetNull),
                    left  => left .HasOne<Product>().WithMany()
                                  .HasForeignKey("Product_Id")
                                  .OnDelete(DeleteBehavior.ClientSetNull),
                    join  =>
                    {
                        join.ToTable("ProductsXCompetenceCenters");
                        join.HasKey("CompetenceCenter_Id", "Product_Id");
                    });

            //
            // Products ↔ Countries
            //
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Countries).WithMany(c => c.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductsXCountries",
                    right => right.HasOne<Country>().WithMany()
                                  .HasForeignKey("Country_Id")
                                  .OnDelete(DeleteBehavior.ClientSetNull),
                    left  => left .HasOne<Product>().WithMany()
                                  .HasForeignKey("Product_Id")
                                  .OnDelete(DeleteBehavior.ClientSetNull),
                    join  =>
                    {
                        join.ToTable("ProductsXCountries");
                        join.HasKey("Country_Id", "Product_Id");
                    });

            //
            // Products ↔ CountryLanguages
            //
            modelBuilder.Entity<Product>()
                .HasMany(p => p.CountryLanguages).WithMany(cl => cl.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductsXCountryLanguages",
                    right => right.HasOne<CountryLanguage>().WithMany()
                                  .HasForeignKey("CountryLanguage_Id")
                                  .OnDelete(DeleteBehavior.ClientSetNull),
                    left  => left .HasOne<Product>().WithMany()
                                  .HasForeignKey("Product_Id")
                                  .OnDelete(DeleteBehavior.ClientSetNull),
                    join  =>
                    {
                        join.ToTable("ProductsXCountryLanguages");
                        join.HasKey("CountryLanguage_Id", "Product_Id");
                    });

            // Products ↔ Locations
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Locations).WithMany(l => l.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductsXLocations",
                    right => right.HasOne<Location>().WithMany()
                        .HasForeignKey("Location_Id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    left => left.HasOne<Product>().WithMany()
                        .HasForeignKey("Product_Id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    join =>
                    {
                        join.ToTable("ProductsXLocations");
                        join.HasKey("Product_Id", "Location_Id");
                    });

            //
            // Products ↔ References
            //
            modelBuilder.Entity<Product>()
                .HasMany(p => p.References).WithMany(r => r.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductsXReferences",
                    right => right.HasOne<Reference>().WithMany()
                                  .HasForeignKey("Reference_Id")
                                  .OnDelete(DeleteBehavior.ClientSetNull),
                    left  => left .HasOne<Product>().WithMany()
                                  .HasForeignKey("Product_Id")
                                  .OnDelete(DeleteBehavior.ClientSetNull),
                    join  =>
                    {
                        join.ToTable("ProductsXReferences");
                        join.HasKey("Reference_Id", "Product_Id");
                    });

            //
            // Products ↔ Taxonomy1 (ProductGroups)
            //
            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductGroups).WithMany(t1 => t1.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductsXProductGroups",
                    right => right.HasOne<Taxonomy1>().WithMany()
                                  .HasForeignKey("Taxonomy1_Id")
                                  .OnDelete(DeleteBehavior.ClientSetNull),
                    left  => left .HasOne<Product>().WithMany()
                                  .HasForeignKey("Product_Id")
                                  .OnDelete(DeleteBehavior.ClientSetNull),
                    join  =>
                    {
                        join.ToTable("ProductsXProductGroups");
                        join.HasKey("Product_Id", "Taxonomy1_Id");
                    });

            //
            // Products ↔ Taxonomy2
            //
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Taxonomy2s).WithMany(t2 => t2.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductsXTaxonomy2",
                    right => right.HasOne<Taxonomy2>().WithMany()
                                  .HasForeignKey("Taxonomy2_Id")
                                  .OnDelete(DeleteBehavior.ClientSetNull),
                    left  => left .HasOne<Product>().WithMany()
                                  .HasForeignKey("Product_Id")
                                  .OnDelete(DeleteBehavior.ClientSetNull),
                    join  =>
                    {
                        join.ToTable("ProductsXTaxonomy2");
                        join.HasKey("Product_Id", "Taxonomy2_Id");
                    });

            //
            // Products ↔ Taxonomy3
            //
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Taxonomy3s).WithMany(t3 => t3.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductsXTaxonomy3",
                    right => right.HasOne<Taxonomy3>().WithMany()
                                  .HasForeignKey("Taxonomy3_Id")
                                  .OnDelete(DeleteBehavior.ClientSetNull),
                    left  => left .HasOne<Product>().WithMany()
                                  .HasForeignKey("Product_Id")
                                  .OnDelete(DeleteBehavior.ClientSetNull),
                    join  =>
                    {
                        join.ToTable("ProductsXTaxonomy3");
                        join.HasKey("Product_Id", "Taxonomy3_Id");
                    });

            //
            // Products ↔ Taxonomy4
            //
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Taxonomy4s).WithMany(t4 => t4.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductsXTaxonomy4",
                    right => right.HasOne<Taxonomy4>().WithMany()
                                  .HasForeignKey("Taxonomy4_Id")
                                  .OnDelete(DeleteBehavior.ClientSetNull),
                    left  => left .HasOne<Product>().WithMany()
                                  .HasForeignKey("Product_Id")
                                  .OnDelete(DeleteBehavior.ClientSetNull),
                    join  =>
                    {
                        join.ToTable("ProductsXTaxonomy4");
                        join.HasKey("Product_Id", "Taxonomy4_Id");
                    });

            //
            // Products ↔ Taxonomy5
            //
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Taxonomy5s).WithMany(t5 => t5.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductsXTaxonomy5",
                    right => right.HasOne<Taxonomy5>().WithMany()
                                  .HasForeignKey("Taxonomy5_Id")
                                  .OnDelete(DeleteBehavior.ClientSetNull),
                    left  => left .HasOne<Product>().WithMany()
                                  .HasForeignKey("Product_Id")
                                  .OnDelete(DeleteBehavior.ClientSetNull),
                    join  =>
                    {
                        join.ToTable("ProductsXTaxonomy5");
                        join.HasKey("Product_Id", "Taxonomy5_Id");
                    });

            //
            // Products ↔ Taxonomy6
            //
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Taxonomy6s).WithMany(t6 => t6.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductsXTaxonomy6",
                    right => right.HasOne<Taxonomy6>().WithMany()
                                  .HasForeignKey("Taxonomy6_Id")
                                  .OnDelete(DeleteBehavior.ClientSetNull),
                    left  => left .HasOne<Product>().WithMany()
                                  .HasForeignKey("Product_Id")
                                  .OnDelete(DeleteBehavior.ClientSetNull),
                    join  =>
                    {
                        join.ToTable("ProductsXTaxonomy6");
                        join.HasKey("Product_Id", "Taxonomy6_Id");
                    });
            
            //
            // CompetenceCenter ↔ Taxonomy1
            //
            modelBuilder.Entity<CompetenceCenter>()
                .HasMany(cc => cc.Taxonomy1s).WithMany(t1 => t1.CompetenceCenters)
                .UsingEntity<Dictionary<string, object>>(
                    "Taxonomy1XCompetenceCenters",
                    right => right.HasOne<Taxonomy1>().WithMany()
                        .HasForeignKey("Taxonomy_Id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    left => left.HasOne<CompetenceCenter>().WithMany()
                        .HasForeignKey("CompetenceCenter_Id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    join =>
                    {
                        join.ToTable("Taxonomy1XCompetenceCenters");
                        join.HasKey("CompetenceCenter_Id", "Taxonomy_Id");
                    });

            //
            // CompetenceCenter ↔ Taxonomy2
            //
            modelBuilder.Entity<CompetenceCenter>()
                .HasMany(cc => cc.Taxonomy2s).WithMany(t2 => t2.CompetenceCenters)
                .UsingEntity<Dictionary<string, object>>(
                    "Taxonomy2XCompetenceCenters",
                    right => right.HasOne<Taxonomy2>().WithMany()
                        .HasForeignKey("Taxonomy_Id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    left => left.HasOne<CompetenceCenter>().WithMany()
                        .HasForeignKey("CompetenceCenter_Id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    join =>
                    {
                        join.ToTable("Taxonomy2XCompetenceCenters");
                        join.HasKey("CompetenceCenter_Id", "Taxonomy_Id");
                    });

            modelBuilder.Entity<CompetenceCenter>()
                .HasMany(cc => cc.Taxonomy3s).WithMany(t3 => t3.CompetenceCenters)
                .UsingEntity<Dictionary<string, object>>(
                    "Taxonomy3XCompetenceCenters",
                    right => right.HasOne<Taxonomy3>().WithMany()
                        .HasForeignKey("Taxonomy_Id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    left => left.HasOne<CompetenceCenter>().WithMany()
                        .HasForeignKey("CompetenceCenter_Id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    join =>
                    {
                        join.ToTable("Taxonomy3XCompetenceCenters");
                        join.HasKey("CompetenceCenter_Id", "Taxonomy_Id");
                    });
            
            modelBuilder.Entity<CompetenceCenter>()
                .HasMany(cc => cc.Taxonomy4s).WithMany(t4 => t4.CompetenceCenters)
                .UsingEntity<Dictionary<string, object>>(
                    "Taxonomy4XCompetenceCenters",
                    right => right.HasOne<Taxonomy4>().WithMany()
                        .HasForeignKey("Taxonomy_Id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    left => left.HasOne<CompetenceCenter>().WithMany()
                        .HasForeignKey("CompetenceCenter_Id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    join =>
                    {
                        join.ToTable("Taxonomy4XCompetenceCenters");
                        join.HasKey("CompetenceCenter_Id", "Taxonomy_Id");
                    });
            
            modelBuilder.Entity<CompetenceCenter>()
                .HasMany(cc => cc.Taxonomy5s).WithMany(t5 => t5.CompetenceCenters)
                .UsingEntity<Dictionary<string, object>>(
                    "Taxonomy5XCompetenceCenters",
                    right => right.HasOne<Taxonomy5>().WithMany()
                        .HasForeignKey("Taxonomy_Id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    left => left.HasOne<CompetenceCenter>().WithMany()
                        .HasForeignKey("CompetenceCenter_Id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    join =>
                    {
                        join.ToTable("Taxonomy5XCompetenceCenters");
                        join.HasKey("CompetenceCenter_Id", "Taxonomy_Id");
                    });

            modelBuilder.Entity<CompetenceCenter>()
                .HasMany(cc => cc.Taxonomy6s).WithMany(t6 => t6.CompetenceCenters)
                .UsingEntity<Dictionary<string, object>>(
                    "Taxonomy6XCompetenceCenters",
                    right => right.HasOne<Taxonomy6>().WithMany()
                        .HasForeignKey("Taxonomy_Id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    left => left.HasOne<CompetenceCenter>().WithMany()
                        .HasForeignKey("CompetenceCenter_Id")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    join =>
                    {
                        join.ToTable("Taxonomy6XCompetenceCenters");
                        join.HasKey("CompetenceCenter_Id", "Taxonomy_Id");
                    });

            //
            // CompetenceCenter ↔ CountryLanguage
            //
            modelBuilder.Entity<CompetenceCenter>()
                .HasMany(cc => cc.CountryLanguages).WithMany(cl => cl.CompetenceCenters)
                .UsingEntity<Dictionary<string, object>>(
                    "CompetenceCentersXCountryLanguages",
                    right => right.HasOne<CountryLanguage>().WithMany()
                                  .HasForeignKey("CountryLanguage_Id")
                                  .OnDelete(DeleteBehavior.Cascade),
                    left  => left .HasOne<CompetenceCenter>().WithMany()
                                  .HasForeignKey("CompetenceCenter_Id")
                                  .OnDelete(DeleteBehavior.Cascade),
                    join  =>
                    {
                        join.ToTable("CompetenceCentersXCountryLanguages");
                        join.HasKey("CompetenceCenter_Id", "CountryLanguage_Id");
                    });

            //
            // One-to-many: ProductCodes
            //
            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductCodes).WithOne(pc => pc.Product)
                .HasForeignKey(pc => pc.Product_Id)
                .OnDelete(DeleteBehavior.ClientSetNull);

            //
            // One-to-many: ProductCharacteristics
            //
            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductCharacteristics).WithOne(ch => ch.Product)
                .HasForeignKey(ch => ch.Product_Id)
                .OnDelete(DeleteBehavior.ClientSetNull);

            //
            // CountryLanguages table itself
            //
            modelBuilder.Entity<CountryLanguage>(entity =>
            {
                entity.ToTable("CountryLanguages");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Countries_Id).HasColumnName("Countries_Id");
                entity.Property(e => e.Languages_Id).HasColumnName("Languages_Id");

                entity.HasOne(d => d.Country).WithMany(c => c.CountryLanguages)
                      .HasForeignKey(d => d.Countries_Id)
                      .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Language).WithMany(l => l.CountryLanguages)
                      .HasForeignKey(d => d.Languages_Id)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            //
            // ProductCode
            //
            modelBuilder.Entity<ProductCode>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.SupplierCode).HasMaxLength(128).IsRequired();
                entity.Property(e => e.EANCode).HasMaxLength(128).IsRequired();
                entity.Property(e => e.Product_Id).HasColumnName("Product_Id");
            });

            //
            // ProductCharacteristic
            //
            modelBuilder.Entity<ProductCharacteristic>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Product_Id).HasColumnName("Product_Id");
                entity.Property(e => e.ProductCode_Id).HasColumnName("ProductCode_Id");

                entity.HasOne(ch => ch.Product).WithMany(p => p.ProductCharacteristics)
                      .HasForeignKey(ch => ch.Product_Id)
                      .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(ch => ch.ProductCode).WithMany(pc => pc.ProductCharacteristics)
                      .HasForeignKey(ch => ch.ProductCode_Id)
                      .OnDelete(DeleteBehavior.ClientSetNull);
            });

            //
            // Brand
            //
            modelBuilder.Entity<Brand>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Name).HasMaxLength(50);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");
            });

            //
            // CompetenceCenter
            //
            modelBuilder.Entity<CompetenceCenter>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Name).HasMaxLength(50);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");
            });

            //
            // Location
            //
            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Name).HasMaxLength(50);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");
            });

            //
            // Reference
            //
            modelBuilder.Entity<Reference>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Name).HasMaxLength(50);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");
            });

            //
            // Taxonomy1
            //
            modelBuilder.Entity<Taxonomy1>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Name).HasMaxLength(50);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");
            });

            //
            // Taxonomy2
            //
            modelBuilder.Entity<Taxonomy2>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Name).HasMaxLength(50);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");
            });

            //
            // Taxonomy3
            //
            modelBuilder.Entity<Taxonomy3>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Name).HasMaxLength(50);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");
            });

            //
            // Taxonomy4
            //
            modelBuilder.Entity<Taxonomy4>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Name).HasMaxLength(50);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");
            });

            //
            // Taxonomy5
            //
            modelBuilder.Entity<Taxonomy5>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Name).HasMaxLength(50);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");
            });

            //
            // Taxonomy6
            //
            modelBuilder.Entity<Taxonomy6>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Name).HasMaxLength(50);
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.LastModifiedOn).HasColumnType("datetime");
            });

            //
            // Country
            //
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

            //
            // Language
            //
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
            
            //
            // Translation entity mapping
            //
            modelBuilder.Entity<Translation>(entity =>
            {
                entity.ToTable("Translations");
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Id).ValueGeneratedNever();
                entity.Property(t => t.LanguageCode)
                    .HasMaxLength(2)
                    .IsFixedLength()
                    .IsRequired();
                entity.Property(t => t.Property)
                    .HasMaxLength(50)
                    .IsRequired();
                entity.Property(t => t.LanguageTranslation)
                    .IsRequired();
                entity.Property(t => t.TranslatableId)
                    .HasColumnName("TranslatableId");
                    
                // Add index for performance
                entity.HasIndex(t => new { t.TranslatableId, t.Property, t.LanguageCode })
                    .HasDatabaseName("TRANSLATION_UNIQUE")
                    .IsUnique();
            });

            // IMPORTANT: Configure entities to ignore Translations navigation property
            // This prevents EF Core from trying to create foreign keys for the polymorphic relationship
            modelBuilder.Entity<AttachmentCategory>()
                .Ignore(ac => ac.Translations);
                
            // If Product entity has Translations navigation property, ignore it too
            modelBuilder.Entity<Product>()
                .Ignore(p => p.Translations);
                
            // Add similar ignore statements for any other entities that have Translations
            // Check your entity classes and add as needed:
            // modelBuilder.Entity<Taxonomy1>().Ignore(t => t.Translations);
            // modelBuilder.Entity<Taxonomy2>().Ignore(t => t.Translations);
            // etc.

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}