using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfCoreValueObjects.Migrations
{
    public partial class TPHForOwnedTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyAddress",
                table: "CompanyAddress");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "CompanyAddress",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyAddress",
                table: "CompanyAddress",
                columns: new[] { "CompanyId", "City", "AddressLine1", "Type" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyAddress",
                table: "CompanyAddress");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "CompanyAddress");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyAddress",
                table: "CompanyAddress",
                columns: new[] { "CompanyId", "City", "AddressLine1" });
        }
    }
}
