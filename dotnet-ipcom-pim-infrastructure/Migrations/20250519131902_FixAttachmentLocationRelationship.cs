using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_ipcom_pim_infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixAttachmentLocationRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Locations_LocationId",
                table: "Attachments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsXLocations",
                table: "ProductsXLocations");

            migrationBuilder.DropIndex(
                name: "IX_ProductsXLocations_Product_Id",
                table: "ProductsXLocations");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_LocationId",
                table: "Attachments");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "Attachments",
                newName: "Location_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsXLocations",
                table: "ProductsXLocations",
                columns: new[] { "Product_Id", "Location_Id" });

            migrationBuilder.CreateTable(
                name: "LocationsXAttachments",
                columns: table => new
                {
                    Attachment_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Location_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationsXAttachments", x => new { x.Attachment_Id, x.Location_Id });
                    table.ForeignKey(
                        name: "FK_LocationsXAttachments_Attachments_Attachment_Id",
                        column: x => x.Attachment_Id,
                        principalTable: "Attachments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationsXAttachments_Locations_Location_Id",
                        column: x => x.Location_Id,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductsXLocations_Location_Id",
                table: "ProductsXLocations",
                column: "Location_Id");

            migrationBuilder.CreateIndex(
                name: "IX_LocationsXAttachments_Location_Id",
                table: "LocationsXAttachments",
                column: "Location_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationsXAttachments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductsXLocations",
                table: "ProductsXLocations");

            migrationBuilder.DropIndex(
                name: "IX_ProductsXLocations_Location_Id",
                table: "ProductsXLocations");

            migrationBuilder.RenameColumn(
                name: "Location_Id",
                table: "Attachments",
                newName: "LocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductsXLocations",
                table: "ProductsXLocations",
                columns: new[] { "Location_Id", "Product_Id" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductsXLocations_Product_Id",
                table: "ProductsXLocations",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_LocationId",
                table: "Attachments",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Locations_LocationId",
                table: "Attachments",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");
        }
    }
}
