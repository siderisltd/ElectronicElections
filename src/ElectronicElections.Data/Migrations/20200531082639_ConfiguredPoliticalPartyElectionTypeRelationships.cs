using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElectronicElections.Data.Migrations
{
    public partial class ConfiguredPoliticalPartyElectionTypeRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PoliticalPartyElectionType",
                columns: table => new
                {
                    PoliticalPartyId = table.Column<Guid>(nullable: false),
                    ElectionTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliticalPartyElectionType", x => new { x.PoliticalPartyId, x.ElectionTypeId });
                    table.ForeignKey(
                        name: "FK_PoliticalPartyElectionType_ElectionTypes_ElectionTypeId",
                        column: x => x.ElectionTypeId,
                        principalTable: "ElectionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PoliticalPartyElectionType_PoliticalParties_PoliticalPartyId",
                        column: x => x.PoliticalPartyId,
                        principalTable: "PoliticalParties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PoliticalPartyElectionType_ElectionTypeId",
                table: "PoliticalPartyElectionType",
                column: "ElectionTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PoliticalPartyElectionType");
        }
    }
}
