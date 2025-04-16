using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobOffersApi.Modules.Companies.Infrastructure.DAL.Migartions
{
    public partial class CompaniesDbContext_AddedLocationToCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location_ApartmentNumber",
                schema: "companies",
                table: "Companies",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location_City",
                schema: "companies",
                table: "Companies",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location_Country",
                schema: "companies",
                table: "Companies",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location_HouseNumber",
                schema: "companies",
                table: "Companies",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location_PostalCode",
                schema: "companies",
                table: "Companies",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location_Street",
                schema: "companies",
                table: "Companies",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location_ApartmentNumber",
                schema: "companies",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Location_City",
                schema: "companies",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Location_Country",
                schema: "companies",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Location_HouseNumber",
                schema: "companies",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Location_PostalCode",
                schema: "companies",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Location_Street",
                schema: "companies",
                table: "Companies");
        }
    }
}
