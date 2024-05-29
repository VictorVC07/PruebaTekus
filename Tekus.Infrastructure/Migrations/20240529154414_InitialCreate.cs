using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tekus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    idcountry = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.idcountry);
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    idprovider = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.idprovider);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    idservice = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    service = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    time_value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.idservice);
                });

            migrationBuilder.CreateTable(
                name: "Providers_has_Services",
                columns: table => new
                {
                    Providers_idprovider = table.Column<int>(type: "int", nullable: false),
                    Services_idservice = table.Column<int>(type: "int", nullable: false),
                    Country_idcountry = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers_has_Services", x => new { x.Providers_idprovider, x.Services_idservice, x.Country_idcountry });
                    table.ForeignKey(
                        name: "FK_Providers_has_Services_Countries_Country_idcountry",
                        column: x => x.Country_idcountry,
                        principalTable: "Countries",
                        principalColumn: "idcountry",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Providers_has_Services_Providers_Providers_idprovider",
                        column: x => x.Providers_idprovider,
                        principalTable: "Providers",
                        principalColumn: "idprovider",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Providers_has_Services_Services_Services_idservice",
                        column: x => x.Services_idservice,
                        principalTable: "Services",
                        principalColumn: "idservice",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Providers_has_Services_Country_idcountry",
                table: "Providers_has_Services",
                column: "Country_idcountry");

            migrationBuilder.CreateIndex(
                name: "IX_Providers_has_Services_Services_idservice",
                table: "Providers_has_Services",
                column: "Services_idservice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Providers_has_Services");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Providers");

            migrationBuilder.DropTable(
                name: "Services");
        }
    }
}
