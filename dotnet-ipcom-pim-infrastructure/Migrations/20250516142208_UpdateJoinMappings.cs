using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_ipcom_pim_infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateJoinMappings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AttachmentCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttachmentCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CountryCode = table.Column<string>(type: "nchar(2)", fixedLength: true, maxLength: 2, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ISOCode = table.Column<string>(type: "nchar(2)", fixedLength: true, maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Logo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyContact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameContact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneContact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailContact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Index = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LocationTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Published = table.Column<bool>(type: "bit", nullable: true),
                    Index = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "References",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Published = table.Column<bool>(type: "bit", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Index = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_References", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Taxonomy1",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Index = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxonomy1", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Taxonomy2",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Index = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxonomy2", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Taxonomy3",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Index = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxonomy3", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Taxonomy4",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Index = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxonomy4", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Taxonomy5",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Index = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxonomy5", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Taxonomy6",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Index = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxonomy6", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Translation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguageCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LanguageTranslation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Property = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TranslatableId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttachmentCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Translation_AttachmentCategories_AttachmentCategoryId",
                        column: x => x.AttachmentCategoryId,
                        principalTable: "AttachmentCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CountryLanguage",
                columns: table => new
                {
                    CountriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguagesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryLanguage", x => new { x.CountriesId, x.LanguagesId });
                    table.ForeignKey(
                        name: "FK_CountryLanguage_Countries_CountriesId",
                        column: x => x.CountriesId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryLanguage_Languages_LanguagesId",
                        column: x => x.LanguagesId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryLanguages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Countries_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Languages_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CountryLanguages_Countries_Countries_Id",
                        column: x => x.Countries_Id,
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CountryLanguages_Languages_Languages_Id",
                        column: x => x.Languages_Id,
                        principalTable: "Languages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LanguageCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Published = table.Column<bool>(type: "bit", nullable: true),
                    Index = table.Column<int>(type: "int", nullable: true),
                    NoResize = table.Column<bool>(type: "bit", nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    ExpiryDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Md5 = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachments_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductCodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierCode = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    EANCode = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Product_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCodes_Products_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductsXBrands",
                columns: table => new
                {
                    Product_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Brand_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsXBrands", x => new { x.Product_Id, x.Brand_Id });
                    table.ForeignKey(
                        name: "FK_ProductsXBrands_Brands_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "Brands",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductsXBrands_Products_Brand_Id",
                        column: x => x.Brand_Id,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductsXCountries",
                columns: table => new
                {
                    Product_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Country_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsXCountries", x => new { x.Product_Id, x.Country_Id });
                    table.ForeignKey(
                        name: "FK_ProductsXCountries_Countries_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "Countries",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductsXCountries_Products_Country_Id",
                        column: x => x.Country_Id,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductsXLocations",
                columns: table => new
                {
                    Product_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Location_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsXLocations", x => new { x.Product_Id, x.Location_Id });
                    table.ForeignKey(
                        name: "FK_ProductsXLocations_Locations_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductsXLocations_Products_Location_Id",
                        column: x => x.Location_Id,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductsXReferences",
                columns: table => new
                {
                    Product_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Reference_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsXReferences", x => new { x.Product_Id, x.Reference_Id });
                    table.ForeignKey(
                        name: "FK_ProductsXReferences_Products_Reference_Id",
                        column: x => x.Reference_Id,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductsXReferences_References_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "References",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductsXProductGroups",
                columns: table => new
                {
                    Product_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Taxonomy1_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsXProductGroups", x => new { x.Product_Id, x.Taxonomy1_Id });
                    table.ForeignKey(
                        name: "FK_ProductsXProductGroups_Products_Taxonomy1_Id",
                        column: x => x.Taxonomy1_Id,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductsXProductGroups_Taxonomy1_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "Taxonomy1",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductsXTaxonomy2",
                columns: table => new
                {
                    Product_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Taxonomy2_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsXTaxonomy2", x => new { x.Product_Id, x.Taxonomy2_Id });
                    table.ForeignKey(
                        name: "FK_ProductsXTaxonomy2_Products_Taxonomy2_Id",
                        column: x => x.Taxonomy2_Id,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductsXTaxonomy2_Taxonomy2_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "Taxonomy2",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductsXTaxonomy3",
                columns: table => new
                {
                    Product_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Taxonomy3_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsXTaxonomy3", x => new { x.Product_Id, x.Taxonomy3_Id });
                    table.ForeignKey(
                        name: "FK_ProductsXTaxonomy3_Products_Taxonomy3_Id",
                        column: x => x.Taxonomy3_Id,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductsXTaxonomy3_Taxonomy3_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "Taxonomy3",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductsXTaxonomy4",
                columns: table => new
                {
                    Product_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Taxonomy4_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsXTaxonomy4", x => new { x.Product_Id, x.Taxonomy4_Id });
                    table.ForeignKey(
                        name: "FK_ProductsXTaxonomy4_Products_Taxonomy4_Id",
                        column: x => x.Taxonomy4_Id,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductsXTaxonomy4_Taxonomy4_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "Taxonomy4",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductsXTaxonomy5",
                columns: table => new
                {
                    Product_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Taxonomy5_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsXTaxonomy5", x => new { x.Product_Id, x.Taxonomy5_Id });
                    table.ForeignKey(
                        name: "FK_ProductsXTaxonomy5_Products_Taxonomy5_Id",
                        column: x => x.Taxonomy5_Id,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductsXTaxonomy5_Taxonomy5_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "Taxonomy5",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompetenceCenters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Taxonomy1Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Taxonomy2Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Taxonomy3Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Taxonomy4Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Taxonomy5Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Taxonomy6Id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetenceCenters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetenceCenters_Taxonomy1_Taxonomy1Id",
                        column: x => x.Taxonomy1Id,
                        principalTable: "Taxonomy1",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompetenceCenters_Taxonomy2_Taxonomy2Id",
                        column: x => x.Taxonomy2Id,
                        principalTable: "Taxonomy2",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompetenceCenters_Taxonomy3_Taxonomy3Id",
                        column: x => x.Taxonomy3Id,
                        principalTable: "Taxonomy3",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompetenceCenters_Taxonomy4_Taxonomy4Id",
                        column: x => x.Taxonomy4Id,
                        principalTable: "Taxonomy4",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompetenceCenters_Taxonomy5_Taxonomy5Id",
                        column: x => x.Taxonomy5Id,
                        principalTable: "Taxonomy5",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CompetenceCenters_Taxonomy6_Taxonomy6Id",
                        column: x => x.Taxonomy6Id,
                        principalTable: "Taxonomy6",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductsXTaxonomy6",
                columns: table => new
                {
                    Product_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Taxonomy6_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsXTaxonomy6", x => new { x.Product_Id, x.Taxonomy6_Id });
                    table.ForeignKey(
                        name: "FK_ProductsXTaxonomy6_Products_Taxonomy6_Id",
                        column: x => x.Taxonomy6_Id,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductsXTaxonomy6_Taxonomy6_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "Taxonomy6",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductsXCountryLanguages",
                columns: table => new
                {
                    Product_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryLanguage_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsXCountryLanguages", x => new { x.Product_Id, x.CountryLanguage_Id });
                    table.ForeignKey(
                        name: "FK_ProductsXCountryLanguages_CountryLanguages_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "CountryLanguages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductsXCountryLanguages_Products_CountryLanguage_Id",
                        column: x => x.CountryLanguage_Id,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AttachmentsXAttachmentCategories",
                columns: table => new
                {
                    Attachment_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttachmentCategory_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttachmentsXAttachmentCategories", x => new { x.Attachment_Id, x.AttachmentCategory_Id });
                    table.ForeignKey(
                        name: "FK_AttachmentsXAttachmentCategories_AttachmentCategories_AttachmentCategory_Id",
                        column: x => x.AttachmentCategory_Id,
                        principalTable: "AttachmentCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttachmentsXAttachmentCategories_Attachments_Attachment_Id",
                        column: x => x.Attachment_Id,
                        principalTable: "Attachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttachmentsXCountries",
                columns: table => new
                {
                    Attachment_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Country_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttachmentsXCountries", x => new { x.Attachment_Id, x.Country_Id });
                    table.ForeignKey(
                        name: "FK_AttachmentsXCountries_Attachments_Country_Id",
                        column: x => x.Country_Id,
                        principalTable: "Attachments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AttachmentsXCountries_Countries_Attachment_Id",
                        column: x => x.Attachment_Id,
                        principalTable: "Countries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductsXAttachments",
                columns: table => new
                {
                    Product_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Attachment_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsXAttachments", x => new { x.Product_Id, x.Attachment_Id });
                    table.ForeignKey(
                        name: "FK_ProductsXAttachments_Attachments_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "Attachments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductsXAttachments_Products_Attachment_Id",
                        column: x => x.Attachment_Id,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductCharacteristics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Width = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Length = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thickness = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diameter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Product_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductCode_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Lambda = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    R = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FireClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EdgeFinish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PressureStrength = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Coating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Density = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Volume = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoolingCapacity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeatingCapacity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PipeDiameter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefrigerantType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DimensionsInnerUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DimensionsOuterUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnergyEfficiency = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCharacteristics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCharacteristics_ProductCodes_ProductCode_Id",
                        column: x => x.ProductCode_Id,
                        principalTable: "ProductCodes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductCharacteristics_Products_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompetenceCenterCountryLanguage",
                columns: table => new
                {
                    CompetenceCentersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryLanguagesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetenceCenterCountryLanguage", x => new { x.CompetenceCentersId, x.CountryLanguagesId });
                    table.ForeignKey(
                        name: "FK_CompetenceCenterCountryLanguage_CompetenceCenters_CompetenceCentersId",
                        column: x => x.CompetenceCentersId,
                        principalTable: "CompetenceCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetenceCenterCountryLanguage_CountryLanguages_CountryLanguagesId",
                        column: x => x.CountryLanguagesId,
                        principalTable: "CountryLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductsXCompetenceCenters",
                columns: table => new
                {
                    Product_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompetenceCenter_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsXCompetenceCenters", x => new { x.Product_Id, x.CompetenceCenter_Id });
                    table.ForeignKey(
                        name: "FK_ProductsXCompetenceCenters_CompetenceCenters_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "CompetenceCenters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductsXCompetenceCenters_Products_CompetenceCenter_Id",
                        column: x => x.CompetenceCenter_Id,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_LocationId",
                table: "Attachments",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentsXAttachmentCategories_AttachmentCategory_Id",
                table: "AttachmentsXAttachmentCategories",
                column: "AttachmentCategory_Id");

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentsXCountries_Country_Id",
                table: "AttachmentsXCountries",
                column: "Country_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CompetenceCenterCountryLanguage_CountryLanguagesId",
                table: "CompetenceCenterCountryLanguage",
                column: "CountryLanguagesId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetenceCenters_Taxonomy1Id",
                table: "CompetenceCenters",
                column: "Taxonomy1Id");

            migrationBuilder.CreateIndex(
                name: "IX_CompetenceCenters_Taxonomy2Id",
                table: "CompetenceCenters",
                column: "Taxonomy2Id");

            migrationBuilder.CreateIndex(
                name: "IX_CompetenceCenters_Taxonomy3Id",
                table: "CompetenceCenters",
                column: "Taxonomy3Id");

            migrationBuilder.CreateIndex(
                name: "IX_CompetenceCenters_Taxonomy4Id",
                table: "CompetenceCenters",
                column: "Taxonomy4Id");

            migrationBuilder.CreateIndex(
                name: "IX_CompetenceCenters_Taxonomy5Id",
                table: "CompetenceCenters",
                column: "Taxonomy5Id");

            migrationBuilder.CreateIndex(
                name: "IX_CompetenceCenters_Taxonomy6Id",
                table: "CompetenceCenters",
                column: "Taxonomy6Id");

            migrationBuilder.CreateIndex(
                name: "IX_CountryLanguage_LanguagesId",
                table: "CountryLanguage",
                column: "LanguagesId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryLanguages_Countries_Id",
                table: "CountryLanguages",
                column: "Countries_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CountryLanguages_Languages_Id",
                table: "CountryLanguages",
                column: "Languages_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCharacteristics_Product_Id",
                table: "ProductCharacteristics",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCharacteristics_ProductCode_Id",
                table: "ProductCharacteristics",
                column: "ProductCode_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCodes_Product_Id",
                table: "ProductCodes",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsXAttachments_Attachment_Id",
                table: "ProductsXAttachments",
                column: "Attachment_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsXBrands_Brand_Id",
                table: "ProductsXBrands",
                column: "Brand_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsXCompetenceCenters_CompetenceCenter_Id",
                table: "ProductsXCompetenceCenters",
                column: "CompetenceCenter_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsXCountries_Country_Id",
                table: "ProductsXCountries",
                column: "Country_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsXCountryLanguages_CountryLanguage_Id",
                table: "ProductsXCountryLanguages",
                column: "CountryLanguage_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsXLocations_Location_Id",
                table: "ProductsXLocations",
                column: "Location_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsXProductGroups_Taxonomy1_Id",
                table: "ProductsXProductGroups",
                column: "Taxonomy1_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsXReferences_Reference_Id",
                table: "ProductsXReferences",
                column: "Reference_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsXTaxonomy2_Taxonomy2_Id",
                table: "ProductsXTaxonomy2",
                column: "Taxonomy2_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsXTaxonomy3_Taxonomy3_Id",
                table: "ProductsXTaxonomy3",
                column: "Taxonomy3_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsXTaxonomy4_Taxonomy4_Id",
                table: "ProductsXTaxonomy4",
                column: "Taxonomy4_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsXTaxonomy5_Taxonomy5_Id",
                table: "ProductsXTaxonomy5",
                column: "Taxonomy5_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsXTaxonomy6_Taxonomy6_Id",
                table: "ProductsXTaxonomy6",
                column: "Taxonomy6_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Translation_AttachmentCategoryId",
                table: "Translation",
                column: "AttachmentCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttachmentsXAttachmentCategories");

            migrationBuilder.DropTable(
                name: "AttachmentsXCountries");

            migrationBuilder.DropTable(
                name: "CompetenceCenterCountryLanguage");

            migrationBuilder.DropTable(
                name: "CountryLanguage");

            migrationBuilder.DropTable(
                name: "ProductCharacteristics");

            migrationBuilder.DropTable(
                name: "ProductsXAttachments");

            migrationBuilder.DropTable(
                name: "ProductsXBrands");

            migrationBuilder.DropTable(
                name: "ProductsXCompetenceCenters");

            migrationBuilder.DropTable(
                name: "ProductsXCountries");

            migrationBuilder.DropTable(
                name: "ProductsXCountryLanguages");

            migrationBuilder.DropTable(
                name: "ProductsXLocations");

            migrationBuilder.DropTable(
                name: "ProductsXProductGroups");

            migrationBuilder.DropTable(
                name: "ProductsXReferences");

            migrationBuilder.DropTable(
                name: "ProductsXTaxonomy2");

            migrationBuilder.DropTable(
                name: "ProductsXTaxonomy3");

            migrationBuilder.DropTable(
                name: "ProductsXTaxonomy4");

            migrationBuilder.DropTable(
                name: "ProductsXTaxonomy5");

            migrationBuilder.DropTable(
                name: "ProductsXTaxonomy6");

            migrationBuilder.DropTable(
                name: "Translation");

            migrationBuilder.DropTable(
                name: "ProductCodes");

            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "CompetenceCenters");

            migrationBuilder.DropTable(
                name: "CountryLanguages");

            migrationBuilder.DropTable(
                name: "References");

            migrationBuilder.DropTable(
                name: "AttachmentCategories");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Taxonomy1");

            migrationBuilder.DropTable(
                name: "Taxonomy2");

            migrationBuilder.DropTable(
                name: "Taxonomy3");

            migrationBuilder.DropTable(
                name: "Taxonomy4");

            migrationBuilder.DropTable(
                name: "Taxonomy5");

            migrationBuilder.DropTable(
                name: "Taxonomy6");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Languages");
        }
    }
}
