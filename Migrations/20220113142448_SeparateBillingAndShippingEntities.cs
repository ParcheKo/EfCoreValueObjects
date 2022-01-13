using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfCoreValueObjects.Migrations
{
    public partial class SeparateBillingAndShippingEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyAddress");

            migrationBuilder.CreateTable(
                name: "BillingAddress",
                columns: table => new
                {
                    City = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillingAddressCustomInfo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingAddress", x => new { x.CompanyId, x.City, x.AddressLine1, x.Type });
                    table.ForeignKey(
                        name: "FK_BillingAddress_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShippingAddress",
                columns: table => new
                {
                    City = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShippingAddressCustomInfo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingAddress", x => new { x.CompanyId, x.City, x.AddressLine1, x.Type });
                    table.ForeignKey(
                        name: "FK_ShippingAddress_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillingAddress");

            migrationBuilder.DropTable(
                name: "ShippingAddress");

            migrationBuilder.CreateTable(
                name: "CompanyAddress",
                columns: table => new
                {
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    City = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyAddress", x => new { x.CompanyId, x.City, x.AddressLine1, x.Type });
                    table.ForeignKey(
                        name: "FK_CompanyAddress_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
