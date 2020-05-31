using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElectronicElections.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ElectionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: false),
                    WikiLink = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PoliticalParties",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: false),
                    Goals = table.Column<string>(maxLength: 500, nullable: true),
                    WikiLink = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliticalParties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Voters",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 20, nullable: false),
                    LastName = table.Column<string>(maxLength: 20, nullable: false),
                    Age = table.Column<byte>(nullable: false),
                    Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voters", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Politicians",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PoliticalPartyId = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 20, nullable: false),
                    LastName = table.Column<string>(maxLength: 20, nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Age = table.Column<byte>(nullable: false),
                    WikiLink = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Politicians", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Politicians_PoliticalParties_PoliticalPartyId",
                        column: x => x.PoliticalPartyId,
                        principalTable: "PoliticalParties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ElectionsType = table.Column<int>(nullable: false),
                    VoterId = table.Column<Guid>(nullable: false),
                    PoliticalPartyId = table.Column<Guid>(nullable: false),
                    VotedFromIp = table.Column<string>(nullable: false),
                    IsVerified = table.Column<bool>(nullable: false),
                    VerificationCode = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    VerifiedOn = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votes_PoliticalParties_PoliticalPartyId",
                        column: x => x.PoliticalPartyId,
                        principalTable: "PoliticalParties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Votes_Voters_VoterId",
                        column: x => x.VoterId,
                        principalTable: "Voters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ElectionTypes",
                columns: new[] { "Id", "Description", "Name", "WikiLink" },
                values: new object[,]
                {
                    { 0, "Some description", "NationalAssembly", "https://google.com" },
                    { 1, "Some description", "PresidentalElections", "https://google.com" },
                    { 2, "Some description", "EuropeanParliament", "https://google.com" }
                });

            migrationBuilder.InsertData(
                table: "PoliticalParties",
                columns: new[] { "Id", "Description", "Goals", "Name", "WikiLink" },
                values: new object[,]
                {
                    { new Guid("ca9ea934-37cd-4d56-9335-0d3348aaf297"), "ГЕРБ е дясноцентристка, популистка, консервативна и проевропейска политическа партия в България. Тя е основана на 3 декември 2006 г. по инициатива на кмета на София Бойко Борисов, на основата на създаденото по-рано през същата година гражданско сдружение с име „Граждани за европейско развитие на България“ и абревиатура „ГЕРБ“.[6] Централата на партията се намира в Националния дворец на културата, на площад „България“ №1 в.", "Унищожаване на Българската икономика и популация. Собствена изгода", "ГЕРБ", "https://google.com" },
                    { new Guid("d5c55e1a-7c1e-47a8-97eb-a0520c62057c"), "Движението за права и свободи (ДПС) е центристка политическа партия в България, ползваща се с подкрепата главно на етническите турци и други мюсюлмани в България, определяща се като либерална партия и член на Либералния интернационал. ДПС е определяно като един от основните поддръжници на олигархичния модел на държавно управление.[1]", "Голове тест 123", "ДПС", "https://google.com" }
                });

            migrationBuilder.InsertData(
                table: "PoliticalPartyElectionType",
                columns: new[] { "PoliticalPartyId", "ElectionTypeId" },
                values: new object[,]
                {
                    { new Guid("ca9ea934-37cd-4d56-9335-0d3348aaf297"), 0 },
                    { new Guid("ca9ea934-37cd-4d56-9335-0d3348aaf297"), 2 },
                    { new Guid("ca9ea934-37cd-4d56-9335-0d3348aaf297"), 1 },
                    { new Guid("d5c55e1a-7c1e-47a8-97eb-a0520c62057c"), 2 }
                });

            migrationBuilder.InsertData(
                table: "Politicians",
                columns: new[] { "Id", "Age", "Description", "FirstName", "LastName", "PoliticalPartyId", "WikiLink" },
                values: new object[,]
                {
                    { new Guid("2206ba49-c68a-426f-8c15-51f5ace48f78"), (byte)50, "Хомосексуалист, който ограбва държавата. Мафиот", "Бойко", "Борисов", new Guid("ca9ea934-37cd-4d56-9335-0d3348aaf297"), "https://google.com" },
                    { new Guid("649b2e11-92a6-4789-9d1b-19ae33722797"), (byte)40, "Тест. Мафиот 2", "Тест", "Тест 2", new Guid("ca9ea934-37cd-4d56-9335-0d3348aaf297"), "https://google.com" },
                    { new Guid("5cf41c11-b33f-485a-9487-1ec005b8d27f"), (byte)20, "Описание дпс", "Тест дпс фн", "Тест дпс лн", new Guid("d5c55e1a-7c1e-47a8-97eb-a0520c62057c"), "https://google.com" },
                    { new Guid("0b9b6f7f-b52b-4e20-ab65-ffe1b107f99f"), (byte)30, "Тест. Описание 2", "Тест дпс 2 фн", "Тест дпс 2 лн", new Guid("d5c55e1a-7c1e-47a8-97eb-a0520c62057c"), "https://google.com" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PoliticalPartyElectionType_ElectionTypeId",
                table: "PoliticalPartyElectionType",
                column: "ElectionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Politicians_PoliticalPartyId",
                table: "Politicians",
                column: "PoliticalPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_PoliticalPartyId",
                table: "Votes",
                column: "PoliticalPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_VoterId",
                table: "Votes",
                column: "VoterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PoliticalPartyElectionType");

            migrationBuilder.DropTable(
                name: "Politicians");

            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "ElectionTypes");

            migrationBuilder.DropTable(
                name: "PoliticalParties");

            migrationBuilder.DropTable(
                name: "Voters");
        }
    }
}
