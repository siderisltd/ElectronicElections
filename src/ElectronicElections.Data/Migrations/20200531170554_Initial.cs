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
                    WikiLink = table.Column<string>(maxLength: 200, nullable: true),
                    LogoLink = table.Column<string>(maxLength: 200, nullable: true)
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
                    WikiLink = table.Column<string>(maxLength: 200, nullable: true),
                    PhotoLink = table.Column<string>(maxLength: 200, nullable: true)
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
                columns: new[] { "Id", "Description", "Goals", "LogoLink", "Name", "WikiLink" },
                values: new object[,]
                {
                    { new Guid("b42d93ab-c675-4809-855f-c903d0c02112"), "ГЕРБ е дясноцентристка, популистка, консервативна и проевропейска политическа партия в България. Тя е основана на 3 декември 2006 г. по инициатива на кмета на София Бойко Борисов, на основата на създаденото по-рано през същата година гражданско сдружение с име „Граждани за европейско развитие на България“ и абревиатура „ГЕРБ“.[6] Централата на партията се намира в Националния дворец на културата, на площад „България“ №1 в.", "Унищожаване на Българската икономика и популация. Собствена изгода", "https://lh3.googleusercontent.com/proxy/4zqzPSm22DJIQ7pdaLVRjJXVfqVXFhx8kaIft_KMNK6S-dVFY7tA0Y4fTUBEvPVMxesXAWYe5P_gID2ANJ8Jb_LwtguNavWm", "ГЕРБ", "https://bg.wikipedia.org/wiki/ГЕРБ" },
                    { new Guid("55755f3e-e9fa-4f01-8882-e09c27042665"), "Движението за права и свободи (ДПС) е центристка политическа партия в България, ползваща се с подкрепата главно на етническите турци и други мюсюлмани в България, определяща се като либерална партия и член на Либералния интернационал. ДПС е определяно като един от основните поддръжници на олигархичния модел на държавно управление.[1]", "Голове тест 123", "https://lh3.googleusercontent.com/proxy/XUQdtdclVg27SSLJxbP1-6An0aSnvFFmwWqZLbf5RhVY-mcxtPrTHYuQmiJu5_jqVPTutOr1OfaR6hZMk9vb2AYe09aqfDRqtkV3Tx3gP72wQHU", "ДПС", "https://bg.wikipedia.org/wiki/ДПС" },
                    { new Guid("197c6090-9ba1-4478-849f-24db488a1e4d"), "„Атака“ е политическа партия в България[2][3], която използва популистки послания, за да спечели симпатии от избирателите.[4] Според някои мнения „Атака“ е крайнодясна партия[1], според други – крайнолява.[5] Заема проруски позиции.[6]", "Партията е парламентарно представена, издава партиен вестник („Атака“) и притежава своя телевизия – „ТВ Алфа“.", "https://pia-news.com/wp-content/uploads/2015/09/ataka_logo.gif", "Атака", "https://bg.wikipedia.org/wiki/Атака_(партия)" }
                });

            migrationBuilder.InsertData(
                table: "PoliticalPartyElectionType",
                columns: new[] { "PoliticalPartyId", "ElectionTypeId" },
                values: new object[,]
                {
                    { new Guid("b42d93ab-c675-4809-855f-c903d0c02112"), 0 },
                    { new Guid("b42d93ab-c675-4809-855f-c903d0c02112"), 2 },
                    { new Guid("b42d93ab-c675-4809-855f-c903d0c02112"), 1 },
                    { new Guid("55755f3e-e9fa-4f01-8882-e09c27042665"), 0 },
                    { new Guid("55755f3e-e9fa-4f01-8882-e09c27042665"), 2 },
                    { new Guid("197c6090-9ba1-4478-849f-24db488a1e4d"), 0 },
                    { new Guid("197c6090-9ba1-4478-849f-24db488a1e4d"), 2 }
                });

            migrationBuilder.InsertData(
                table: "Politicians",
                columns: new[] { "Id", "Age", "Description", "FirstName", "LastName", "PhotoLink", "PoliticalPartyId", "WikiLink" },
                values: new object[,]
                {
                    { new Guid("c99a57e8-e41b-41d9-b41b-7959a98daee7"), (byte)50, "Хомосексуалист, който ограбва държавата. Мафиот", "Бойко", "Борисов", null, new Guid("b42d93ab-c675-4809-855f-c903d0c02112"), "https://google.com" },
                    { new Guid("b4b9c11c-a08b-4c0e-81d7-61dff06690a1"), (byte)40, "Тест. Мафиот 2", "Тест", "Тест 2", null, new Guid("b42d93ab-c675-4809-855f-c903d0c02112"), "https://google.com" },
                    { new Guid("a6e36a45-7f1d-46a8-b003-37f616350ef6"), (byte)20, "Описание дпс", "Тест дпс фн", "Тест дпс лн", null, new Guid("55755f3e-e9fa-4f01-8882-e09c27042665"), "https://google.com" },
                    { new Guid("6d93ab01-b55c-4d14-a571-1c254e32b32e"), (byte)30, "Тест. Описание 2", "Тест дпс 2 фн", "Тест дпс 2 лн", null, new Guid("55755f3e-e9fa-4f01-8882-e09c27042665"), "https://google.com" },
                    { new Guid("04f446de-3a3f-4d01-ae3e-2074a29d847e"), (byte)60, "Активист", "Волен", "Сидеров", null, new Guid("197c6090-9ba1-4478-849f-24db488a1e4d"), "https://google.com" }
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
