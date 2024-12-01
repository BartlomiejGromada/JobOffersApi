using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobOffersApi.Modules.JobOffers.Infrastructure.DAL.Migartions
{
    public partial class JobOffersDbContext_InitSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "jobOffers");

            migrationBuilder.CreateTable(
                name: "JobAttributes",
                schema: "jobOffers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobAttributes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobOffers",
                schema: "jobOffers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    DescriptionHtml = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location_Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location_City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location_Street = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Location_HouseNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Location_ApartmentNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Location_PostalCode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOffers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobApplications",
                schema: "jobOffers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CandidateFirstName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CandidateLastName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    JobOfferId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MessageToEmployer = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Disposition_Availability = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Disposition_Date = table.Column<DateTime>(type: "date", nullable: true),
                    FinancialExpectations_Value_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FinancialExpectations_Value_Currency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FinancialExpectations_SalaryType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FinancialExpectations_SalaryPeriod = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PreferredContract = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CV = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobApplications_JobOffers_JobOfferId",
                        column: x => x.JobOfferId,
                        principalSchema: "jobOffers",
                        principalTable: "JobOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobAttributeJobOffer",
                schema: "jobOffers",
                columns: table => new
                {
                    JobAttributesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobOffersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobAttributeJobOffer", x => new { x.JobAttributesId, x.JobOffersId });
                    table.ForeignKey(
                        name: "FK_JobAttributeJobOffer_JobAttributes_JobAttributesId",
                        column: x => x.JobAttributesId,
                        principalSchema: "jobOffers",
                        principalTable: "JobAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobAttributeJobOffer_JobOffers_JobOffersId",
                        column: x => x.JobOffersId,
                        principalSchema: "jobOffers",
                        principalTable: "JobOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobOffers_FinancialConditions",
                schema: "jobOffers",
                columns: table => new
                {
                    JobOfferId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Value_Currency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SalaryType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SalaryPeriod = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOffers_FinancialConditions", x => new { x.JobOfferId, x.Id });
                    table.ForeignKey(
                        name: "FK_JobOffers_FinancialConditions_JobOffers_JobOfferId",
                        column: x => x.JobOfferId,
                        principalSchema: "jobOffers",
                        principalTable: "JobOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_JobOfferId",
                schema: "jobOffers",
                table: "JobApplications",
                column: "JobOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_JobAttributeJobOffer_JobOffersId",
                schema: "jobOffers",
                table: "JobAttributeJobOffer",
                column: "JobOffersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobApplications",
                schema: "jobOffers");

            migrationBuilder.DropTable(
                name: "JobAttributeJobOffer",
                schema: "jobOffers");

            migrationBuilder.DropTable(
                name: "JobOffers_FinancialConditions",
                schema: "jobOffers");

            migrationBuilder.DropTable(
                name: "JobAttributes",
                schema: "jobOffers");

            migrationBuilder.DropTable(
                name: "JobOffers",
                schema: "jobOffers");
        }
    }
}
