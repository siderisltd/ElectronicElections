using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElectronicElections.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: false),
                    Goals = table.Column<string>(maxLength: 500, nullable: true),
                    WikiLink = table.Column<string>(maxLength: 200, nullable: true),
                    ImgLink = table.Column<string>(maxLength: 200, nullable: false),
                    CandidateType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Id);
                });

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
                name: "Voters",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    Age = table.Column<byte>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    IpInfo = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CandidateElectionType",
                columns: table => new
                {
                    CandidateId = table.Column<Guid>(nullable: false),
                    ElectionTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateElectionType", x => new { x.CandidateId, x.ElectionTypeId });
                    table.ForeignKey(
                        name: "FK_CandidateElectionType_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateElectionType_ElectionTypes_ElectionTypeId",
                        column: x => x.ElectionTypeId,
                        principalTable: "ElectionTypes",
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
                    CandidateId = table.Column<Guid>(nullable: false),
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
                        name: "FK_Votes_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidates",
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
                table: "Candidates",
                columns: new[] { "Id", "CandidateType", "Description", "Goals", "ImgLink", "Name", "WikiLink" },
                values: new object[,]
                {
                    { new Guid("df70469c-9d91-4390-8cca-e0e3b5d03e51"), 1, "ГЕРБ е дясноцентристка, популистка, консервативна и проевропейска политическа партия в България. Тя е основана на 3 декември 2006 г. по инициатива на кмета на София Бойко Борисов, на основата на създаденото по-рано през същата година гражданско сдружение с име „Граждани за европейско развитие на България“ и абревиатура „ГЕРБ“.[6] Централата на партията се намира в Националния дворец на културата, на площад „България“ №1 в.", "Унищожаване на Българската икономика и популация. Собствена изгода", "https://m.netinfo.bg/media/images/34784/34784806/991-ratio-kotka-kuche.jpg", "ГЕРБ", "https://m.netinfo.bg/media/images/34784/34784806/991-ratio-kotka-kuche.jpg" },
                    { new Guid("53b319cb-b0e0-4f22-af56-2fd86384361f"), 1, "Движението за права и свободи (ДПС) е центристка политическа партия в България, ползваща се с подкрепата главно на етническите турци и други мюсюлмани в България, определяща се като либерална партия и член на Либералния интернационал. ДПС е определяно като един от основните поддръжници на олигархичния модел на държавно управление.[1]", "Голове тест 123", "https://m5.netinfo.bg/media/images/15946/15946663/896-504-kuche-i-kote.jpg", "ДПС", "https://bg.wikipedia.org/wiki/ДПС" },
                    { new Guid("24fe9645-55b7-4a09-ac60-ca2548a61899"), 1, "„Атака“ е политическа партия в България[2][3], която използва популистки послания, за да спечели симпатии от избирателите.[4] Според някои мнения „Атака“ е крайнодясна партия[1], според други – крайнолява.[5] Заема проруски позиции.[6]", "Партията е парламентарно представена, издава партиен вестник („Атака“) и притежава своя телевизия – „ТВ Алфа“.", "https://m.netinfo.bg/media/images/32905/32905551/991-ratio-kotki-i-kucheta.jpg", "Атака", "https://bg.wikipedia.org/wiki/Атака_(партия)" },
                    { new Guid("78823942-b5d5-4377-80c6-742d81362625"), 0, "Тест инфо", "Партията е парламентарно представена, издава партиен вестник („Атака“) и притежава своя телевизия – „ТВ Алфа“.", "https://static.framar.bg/thumbs/6/lifestyle/usmivka-kuche.png", "Волен Сидеров", "https://bg.wikipedia.org/wiki/Волен_Сидеров" }
                });

            migrationBuilder.InsertData(
                table: "ElectionTypes",
                columns: new[] { "Id", "Description", "Name", "WikiLink" },
                values: new object[,]
                {
                    { 0, "Избори за народно събрание", "NationalAssembly", "https://bg.wikipedia.org/wiki/Избори_в_България#За_народно_събрание" },
                    { 1, "Президентски избори", "PresidentalElections", "https://bg.wikipedia.org/wiki/Избори_в_България#За_президент" },
                    { 2, "Избори за европейски парламент", "EuropeanParliament", "https://bg.wikipedia.org/wiki/Избори_в_България#За_европейски_парламент" }
                });

            migrationBuilder.InsertData(
                table: "CandidateElectionType",
                columns: new[] { "CandidateId", "ElectionTypeId" },
                values: new object[,]
                {
                    { new Guid("df70469c-9d91-4390-8cca-e0e3b5d03e51"), 0 },
                    { new Guid("24fe9645-55b7-4a09-ac60-ca2548a61899"), 0 },
                    { new Guid("53b319cb-b0e0-4f22-af56-2fd86384361f"), 0 },
                    { new Guid("df70469c-9d91-4390-8cca-e0e3b5d03e51"), 1 },
                    { new Guid("78823942-b5d5-4377-80c6-742d81362625"), 1 },
                    { new Guid("df70469c-9d91-4390-8cca-e0e3b5d03e51"), 2 },
                    { new Guid("24fe9645-55b7-4a09-ac60-ca2548a61899"), 2 },
                    { new Guid("53b319cb-b0e0-4f22-af56-2fd86384361f"), 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidateElectionType_ElectionTypeId",
                table: "CandidateElectionType",
                column: "ElectionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_CandidateId",
                table: "Votes",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_VoterId",
                table: "Votes",
                column: "VoterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateElectionType");

            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "ElectionTypes");

            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "Voters");
        }
    }
}
