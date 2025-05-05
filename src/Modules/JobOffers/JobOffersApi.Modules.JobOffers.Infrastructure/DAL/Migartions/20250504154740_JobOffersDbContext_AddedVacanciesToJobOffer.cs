using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobOffersApi.Modules.JobOffers.Infrastructure.DAL.Migartions
{
    /// <inheritdoc />
    public partial class JobOffersDbContext_AddedVacanciesToJobOffer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Vacancies",
                schema: "jobOffers",
                table: "JobOffers",
                type: "int",
                nullable: false,
                defaultValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Vacancies",
                schema: "jobOffers",
                table: "JobOffers");
        }
    }
}
