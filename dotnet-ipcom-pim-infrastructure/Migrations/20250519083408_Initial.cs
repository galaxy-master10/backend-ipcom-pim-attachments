using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_ipcom_pim_infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Locations_Location_Id",
                table: "Attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_AttachmentsXCountries_Attachments_Country_Id",
                table: "AttachmentsXCountries");

            migrationBuilder.DropForeignKey(
                name: "FK_AttachmentsXCountries_Countries_Attachment_Id",
                table: "AttachmentsXCountries");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXAttachments_Attachments_Product_Id",
                table: "ProductsXAttachments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXAttachments_Products_Attachment_Id",
                table: "ProductsXAttachments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXBrands_Brands_Product_Id",
                table: "ProductsXBrands");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXBrands_Products_Brand_Id",
                table: "ProductsXBrands");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXCompetenceCenters_CompetenceCenters_Product_Id",
                table: "ProductsXCompetenceCenters");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXCompetenceCenters_Products_CompetenceCenter_Id",
                table: "ProductsXCompetenceCenters");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXCountries_Countries_Product_Id",
                table: "ProductsXCountries");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXCountries_Products_Country_Id",
                table: "ProductsXCountries");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXCountryLanguages_CountryLanguages_Product_Id",
                table: "ProductsXCountryLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXCountryLanguages_Products_CountryLanguage_Id",
                table: "ProductsXCountryLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXLocations_Locations_Product_Id",
                table: "ProductsXLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXLocations_Products_Location_Id",
                table: "ProductsXLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXProductGroups_Products_Taxonomy1_Id",
                table: "ProductsXProductGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXProductGroups_Taxonomy1_Product_Id",
                table: "ProductsXProductGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXReferences_Products_Reference_Id",
                table: "ProductsXReferences");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXReferences_References_Product_Id",
                table: "ProductsXReferences");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXTaxonomy2_Products_Taxonomy2_Id",
                table: "ProductsXTaxonomy2");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXTaxonomy2_Taxonomy2_Product_Id",
                table: "ProductsXTaxonomy2");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXTaxonomy3_Products_Taxonomy3_Id",
                table: "ProductsXTaxonomy3");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXTaxonomy3_Taxonomy3_Product_Id",
                table: "ProductsXTaxonomy3");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXTaxonomy4_Products_Taxonomy4_Id",
                table: "ProductsXTaxonomy4");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXTaxonomy4_Taxonomy4_Product_Id",
                table: "ProductsXTaxonomy4");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXTaxonomy5_Products_Taxonomy5_Id",
                table: "ProductsXTaxonomy5");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXTaxonomy5_Taxonomy5_Product_Id",
                table: "ProductsXTaxonomy5");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXTaxonomy6_Products_Taxonomy6_Id",
                table: "ProductsXTaxonomy6");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXTaxonomy6_Taxonomy6_Product_Id",
                table: "ProductsXTaxonomy6");

            migrationBuilder.DropTable(
                name: "CompetenceCenterCountryLanguage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsXReferences",
                table: "ProductsXReferences");

            migrationBuilder.DropIndex(
                name: "IX_ProductsXReferences_Reference_Id",
                table: "ProductsXReferences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsXLocations",
                table: "ProductsXLocations");

            migrationBuilder.DropIndex(
                name: "IX_ProductsXLocations_Location_Id",
                table: "ProductsXLocations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsXCountryLanguages",
                table: "ProductsXCountryLanguages");

            migrationBuilder.DropIndex(
                name: "IX_ProductsXCountryLanguages_CountryLanguage_Id",
                table: "ProductsXCountryLanguages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsXCountries",
                table: "ProductsXCountries");

            migrationBuilder.DropIndex(
                name: "IX_ProductsXCountries_Country_Id",
                table: "ProductsXCountries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsXCompetenceCenters",
                table: "ProductsXCompetenceCenters");

            migrationBuilder.DropIndex(
                name: "IX_ProductsXCompetenceCenters_CompetenceCenter_Id",
                table: "ProductsXCompetenceCenters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsXBrands",
                table: "ProductsXBrands");

            migrationBuilder.DropIndex(
                name: "IX_ProductsXBrands_Brand_Id",
                table: "ProductsXBrands");

            migrationBuilder.RenameColumn(
                name: "Location_Id",
                table: "Attachments",
                newName: "LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Attachments_Location_Id",
                table: "Attachments",
                newName: "IX_Attachments_LocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsXReferences",
                table: "ProductsXReferences",
                columns: new[] { "Reference_Id", "Product_Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsXLocations",
                table: "ProductsXLocations",
                columns: new[] { "Location_Id", "Product_Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsXCountryLanguages",
                table: "ProductsXCountryLanguages",
                columns: new[] { "CountryLanguage_Id", "Product_Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsXCountries",
                table: "ProductsXCountries",
                columns: new[] { "Country_Id", "Product_Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsXCompetenceCenters",
                table: "ProductsXCompetenceCenters",
                columns: new[] { "CompetenceCenter_Id", "Product_Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsXBrands",
                table: "ProductsXBrands",
                columns: new[] { "Brand_Id", "Product_Id" });

            migrationBuilder.CreateTable(
                name: "CompetenceCentersXCountryLanguages",
                columns: table => new
                {
                    CompetenceCenter_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryLanguage_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetenceCentersXCountryLanguages", x => new { x.CompetenceCenter_Id, x.CountryLanguage_Id });
                    table.ForeignKey(
                        name: "FK_CompetenceCentersXCountryLanguages_CompetenceCenters_CompetenceCenter_Id",
                        column: x => x.CompetenceCenter_Id,
                        principalTable: "CompetenceCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompetenceCentersXCountryLanguages_CountryLanguages_CountryLanguage_Id",
                        column: x => x.CountryLanguage_Id,
                        principalTable: "CountryLanguages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductsXReferences_Product_Id",
                table: "ProductsXReferences",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsXLocations_Product_Id",
                table: "ProductsXLocations",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsXCountryLanguages_Product_Id",
                table: "ProductsXCountryLanguages",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsXCountries_Product_Id",
                table: "ProductsXCountries",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsXCompetenceCenters_Product_Id",
                table: "ProductsXCompetenceCenters",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsXBrands_Product_Id",
                table: "ProductsXBrands",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CompetenceCentersXCountryLanguages_CountryLanguage_Id",
                table: "CompetenceCentersXCountryLanguages",
                column: "CountryLanguage_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Locations_LocationId",
                table: "Attachments",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttachmentsXCountries_Attachments_Attachment_Id",
                table: "AttachmentsXCountries",
                column: "Attachment_Id",
                principalTable: "Attachments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttachmentsXCountries_Countries_Country_Id",
                table: "AttachmentsXCountries",
                column: "Country_Id",
                principalTable: "Countries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXAttachments_Attachments_Attachment_Id",
                table: "ProductsXAttachments",
                column: "Attachment_Id",
                principalTable: "Attachments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXAttachments_Products_Product_Id",
                table: "ProductsXAttachments",
                column: "Product_Id",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXBrands_Brands_Brand_Id",
                table: "ProductsXBrands",
                column: "Brand_Id",
                principalTable: "Brands",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXBrands_Products_Product_Id",
                table: "ProductsXBrands",
                column: "Product_Id",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXCompetenceCenters_CompetenceCenters_CompetenceCenter_Id",
                table: "ProductsXCompetenceCenters",
                column: "CompetenceCenter_Id",
                principalTable: "CompetenceCenters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXCompetenceCenters_Products_Product_Id",
                table: "ProductsXCompetenceCenters",
                column: "Product_Id",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXCountries_Countries_Country_Id",
                table: "ProductsXCountries",
                column: "Country_Id",
                principalTable: "Countries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXCountries_Products_Product_Id",
                table: "ProductsXCountries",
                column: "Product_Id",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXCountryLanguages_CountryLanguages_CountryLanguage_Id",
                table: "ProductsXCountryLanguages",
                column: "CountryLanguage_Id",
                principalTable: "CountryLanguages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXCountryLanguages_Products_Product_Id",
                table: "ProductsXCountryLanguages",
                column: "Product_Id",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXLocations_Locations_Location_Id",
                table: "ProductsXLocations",
                column: "Location_Id",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXLocations_Products_Product_Id",
                table: "ProductsXLocations",
                column: "Product_Id",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXProductGroups_Products_Product_Id",
                table: "ProductsXProductGroups",
                column: "Product_Id",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXProductGroups_Taxonomy1_Taxonomy1_Id",
                table: "ProductsXProductGroups",
                column: "Taxonomy1_Id",
                principalTable: "Taxonomy1",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXReferences_Products_Product_Id",
                table: "ProductsXReferences",
                column: "Product_Id",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXReferences_References_Reference_Id",
                table: "ProductsXReferences",
                column: "Reference_Id",
                principalTable: "References",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXTaxonomy2_Products_Product_Id",
                table: "ProductsXTaxonomy2",
                column: "Product_Id",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXTaxonomy2_Taxonomy2_Taxonomy2_Id",
                table: "ProductsXTaxonomy2",
                column: "Taxonomy2_Id",
                principalTable: "Taxonomy2",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXTaxonomy3_Products_Product_Id",
                table: "ProductsXTaxonomy3",
                column: "Product_Id",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXTaxonomy3_Taxonomy3_Taxonomy3_Id",
                table: "ProductsXTaxonomy3",
                column: "Taxonomy3_Id",
                principalTable: "Taxonomy3",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXTaxonomy4_Products_Product_Id",
                table: "ProductsXTaxonomy4",
                column: "Product_Id",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXTaxonomy4_Taxonomy4_Taxonomy4_Id",
                table: "ProductsXTaxonomy4",
                column: "Taxonomy4_Id",
                principalTable: "Taxonomy4",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXTaxonomy5_Products_Product_Id",
                table: "ProductsXTaxonomy5",
                column: "Product_Id",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXTaxonomy5_Taxonomy5_Taxonomy5_Id",
                table: "ProductsXTaxonomy5",
                column: "Taxonomy5_Id",
                principalTable: "Taxonomy5",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXTaxonomy6_Products_Product_Id",
                table: "ProductsXTaxonomy6",
                column: "Product_Id",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXTaxonomy6_Taxonomy6_Taxonomy6_Id",
                table: "ProductsXTaxonomy6",
                column: "Taxonomy6_Id",
                principalTable: "Taxonomy6",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Locations_LocationId",
                table: "Attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_AttachmentsXCountries_Attachments_Attachment_Id",
                table: "AttachmentsXCountries");

            migrationBuilder.DropForeignKey(
                name: "FK_AttachmentsXCountries_Countries_Country_Id",
                table: "AttachmentsXCountries");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXAttachments_Attachments_Attachment_Id",
                table: "ProductsXAttachments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXAttachments_Products_Product_Id",
                table: "ProductsXAttachments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXBrands_Brands_Brand_Id",
                table: "ProductsXBrands");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXBrands_Products_Product_Id",
                table: "ProductsXBrands");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXCompetenceCenters_CompetenceCenters_CompetenceCenter_Id",
                table: "ProductsXCompetenceCenters");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXCompetenceCenters_Products_Product_Id",
                table: "ProductsXCompetenceCenters");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXCountries_Countries_Country_Id",
                table: "ProductsXCountries");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXCountries_Products_Product_Id",
                table: "ProductsXCountries");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXCountryLanguages_CountryLanguages_CountryLanguage_Id",
                table: "ProductsXCountryLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXCountryLanguages_Products_Product_Id",
                table: "ProductsXCountryLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXLocations_Locations_Location_Id",
                table: "ProductsXLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXLocations_Products_Product_Id",
                table: "ProductsXLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXProductGroups_Products_Product_Id",
                table: "ProductsXProductGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXProductGroups_Taxonomy1_Taxonomy1_Id",
                table: "ProductsXProductGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXReferences_Products_Product_Id",
                table: "ProductsXReferences");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXReferences_References_Reference_Id",
                table: "ProductsXReferences");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXTaxonomy2_Products_Product_Id",
                table: "ProductsXTaxonomy2");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXTaxonomy2_Taxonomy2_Taxonomy2_Id",
                table: "ProductsXTaxonomy2");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXTaxonomy3_Products_Product_Id",
                table: "ProductsXTaxonomy3");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXTaxonomy3_Taxonomy3_Taxonomy3_Id",
                table: "ProductsXTaxonomy3");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXTaxonomy4_Products_Product_Id",
                table: "ProductsXTaxonomy4");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXTaxonomy4_Taxonomy4_Taxonomy4_Id",
                table: "ProductsXTaxonomy4");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXTaxonomy5_Products_Product_Id",
                table: "ProductsXTaxonomy5");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXTaxonomy5_Taxonomy5_Taxonomy5_Id",
                table: "ProductsXTaxonomy5");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXTaxonomy6_Products_Product_Id",
                table: "ProductsXTaxonomy6");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsXTaxonomy6_Taxonomy6_Taxonomy6_Id",
                table: "ProductsXTaxonomy6");

            migrationBuilder.DropTable(
                name: "CompetenceCentersXCountryLanguages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsXReferences",
                table: "ProductsXReferences");

            migrationBuilder.DropIndex(
                name: "IX_ProductsXReferences_Product_Id",
                table: "ProductsXReferences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsXLocations",
                table: "ProductsXLocations");

            migrationBuilder.DropIndex(
                name: "IX_ProductsXLocations_Product_Id",
                table: "ProductsXLocations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsXCountryLanguages",
                table: "ProductsXCountryLanguages");

            migrationBuilder.DropIndex(
                name: "IX_ProductsXCountryLanguages_Product_Id",
                table: "ProductsXCountryLanguages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsXCountries",
                table: "ProductsXCountries");

            migrationBuilder.DropIndex(
                name: "IX_ProductsXCountries_Product_Id",
                table: "ProductsXCountries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsXCompetenceCenters",
                table: "ProductsXCompetenceCenters");

            migrationBuilder.DropIndex(
                name: "IX_ProductsXCompetenceCenters_Product_Id",
                table: "ProductsXCompetenceCenters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsXBrands",
                table: "ProductsXBrands");

            migrationBuilder.DropIndex(
                name: "IX_ProductsXBrands_Product_Id",
                table: "ProductsXBrands");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "Attachments",
                newName: "Location_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Attachments_LocationId",
                table: "Attachments",
                newName: "IX_Attachments_Location_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsXReferences",
                table: "ProductsXReferences",
                columns: new[] { "Product_Id", "Reference_Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsXLocations",
                table: "ProductsXLocations",
                columns: new[] { "Product_Id", "Location_Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsXCountryLanguages",
                table: "ProductsXCountryLanguages",
                columns: new[] { "Product_Id", "CountryLanguage_Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsXCountries",
                table: "ProductsXCountries",
                columns: new[] { "Product_Id", "Country_Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsXCompetenceCenters",
                table: "ProductsXCompetenceCenters",
                columns: new[] { "Product_Id", "CompetenceCenter_Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsXBrands",
                table: "ProductsXBrands",
                columns: new[] { "Product_Id", "Brand_Id" });

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

            migrationBuilder.CreateIndex(
                name: "IX_ProductsXReferences_Reference_Id",
                table: "ProductsXReferences",
                column: "Reference_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsXLocations_Location_Id",
                table: "ProductsXLocations",
                column: "Location_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsXCountryLanguages_CountryLanguage_Id",
                table: "ProductsXCountryLanguages",
                column: "CountryLanguage_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsXCountries_Country_Id",
                table: "ProductsXCountries",
                column: "Country_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsXCompetenceCenters_CompetenceCenter_Id",
                table: "ProductsXCompetenceCenters",
                column: "CompetenceCenter_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsXBrands_Brand_Id",
                table: "ProductsXBrands",
                column: "Brand_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CompetenceCenterCountryLanguage_CountryLanguagesId",
                table: "CompetenceCenterCountryLanguage",
                column: "CountryLanguagesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Locations_Location_Id",
                table: "Attachments",
                column: "Location_Id",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttachmentsXCountries_Attachments_Country_Id",
                table: "AttachmentsXCountries",
                column: "Country_Id",
                principalTable: "Attachments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttachmentsXCountries_Countries_Attachment_Id",
                table: "AttachmentsXCountries",
                column: "Attachment_Id",
                principalTable: "Countries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXAttachments_Attachments_Product_Id",
                table: "ProductsXAttachments",
                column: "Product_Id",
                principalTable: "Attachments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXAttachments_Products_Attachment_Id",
                table: "ProductsXAttachments",
                column: "Attachment_Id",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXBrands_Brands_Product_Id",
                table: "ProductsXBrands",
                column: "Product_Id",
                principalTable: "Brands",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXBrands_Products_Brand_Id",
                table: "ProductsXBrands",
                column: "Brand_Id",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXCompetenceCenters_CompetenceCenters_Product_Id",
                table: "ProductsXCompetenceCenters",
                column: "Product_Id",
                principalTable: "CompetenceCenters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXCompetenceCenters_Products_CompetenceCenter_Id",
                table: "ProductsXCompetenceCenters",
                column: "CompetenceCenter_Id",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXCountries_Countries_Product_Id",
                table: "ProductsXCountries",
                column: "Product_Id",
                principalTable: "Countries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXCountries_Products_Country_Id",
                table: "ProductsXCountries",
                column: "Country_Id",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXCountryLanguages_CountryLanguages_Product_Id",
                table: "ProductsXCountryLanguages",
                column: "Product_Id",
                principalTable: "CountryLanguages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXCountryLanguages_Products_CountryLanguage_Id",
                table: "ProductsXCountryLanguages",
                column: "CountryLanguage_Id",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXLocations_Locations_Product_Id",
                table: "ProductsXLocations",
                column: "Product_Id",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXLocations_Products_Location_Id",
                table: "ProductsXLocations",
                column: "Location_Id",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXProductGroups_Products_Taxonomy1_Id",
                table: "ProductsXProductGroups",
                column: "Taxonomy1_Id",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXProductGroups_Taxonomy1_Product_Id",
                table: "ProductsXProductGroups",
                column: "Product_Id",
                principalTable: "Taxonomy1",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXReferences_Products_Reference_Id",
                table: "ProductsXReferences",
                column: "Reference_Id",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXReferences_References_Product_Id",
                table: "ProductsXReferences",
                column: "Product_Id",
                principalTable: "References",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXTaxonomy2_Products_Taxonomy2_Id",
                table: "ProductsXTaxonomy2",
                column: "Taxonomy2_Id",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXTaxonomy2_Taxonomy2_Product_Id",
                table: "ProductsXTaxonomy2",
                column: "Product_Id",
                principalTable: "Taxonomy2",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXTaxonomy3_Products_Taxonomy3_Id",
                table: "ProductsXTaxonomy3",
                column: "Taxonomy3_Id",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXTaxonomy3_Taxonomy3_Product_Id",
                table: "ProductsXTaxonomy3",
                column: "Product_Id",
                principalTable: "Taxonomy3",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXTaxonomy4_Products_Taxonomy4_Id",
                table: "ProductsXTaxonomy4",
                column: "Taxonomy4_Id",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXTaxonomy4_Taxonomy4_Product_Id",
                table: "ProductsXTaxonomy4",
                column: "Product_Id",
                principalTable: "Taxonomy4",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXTaxonomy5_Products_Taxonomy5_Id",
                table: "ProductsXTaxonomy5",
                column: "Taxonomy5_Id",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXTaxonomy5_Taxonomy5_Product_Id",
                table: "ProductsXTaxonomy5",
                column: "Product_Id",
                principalTable: "Taxonomy5",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXTaxonomy6_Products_Taxonomy6_Id",
                table: "ProductsXTaxonomy6",
                column: "Taxonomy6_Id",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsXTaxonomy6_Taxonomy6_Product_Id",
                table: "ProductsXTaxonomy6",
                column: "Product_Id",
                principalTable: "Taxonomy6",
                principalColumn: "Id");
        }
    }
}
