using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

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
                    service = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    Country_idcountry = table.Column<int>(type: "int", nullable: false),
                    time_value = table.Column<float>(type: "real", nullable: false)
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

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "idcountry", "country" },
                values: new object[,]
                {
                    { 1, "Colombia" },
                    { 2, "United States" },
                    { 3, "Canada" },
                    { 4, "Mexico" },
                    { 5, "Brazil" },
                    { 6, "Argentina" },
                    { 7, "Chile" },
                    { 8, "Peru" },
                    { 9, "Venezuela" },
                    { 10, "Uruguay" }
                });

            migrationBuilder.InsertData(
                table: "Providers",
                columns: new[] { "idprovider", "mail", "name", "nit" },
                values: new object[,]
                {
                    { 1, "tecnisoft@gmail.com", "Tecnisoft", "900123456-1" },
                    { 2, "servitech@yahoo.com", "ServiTech", "900234567-2" },
                    { 3, "solucionesit@hotmail.com", "Soluciones IT", "900345678-3" },
                    { 4, "digitalware@gmail.com", "DigitalWare", "900456789-4" },
                    { 5, "techpro@yahoo.com", "TechPro", "900567890-5" },
                    { 6, "infotech@hotmail.com", "InfoTech", "900678901-6" },
                    { 7, "cybersoft@gmail.com", "CyberSoft", "900789012-7" },
                    { 8, "compunet@yahoo.com", "CompuNet", "900890123-8" },
                    { 9, "netsolutions@hotmail.com", "NetSolutions", "900901234-9" },
                    { 10, "softtech@gmail.com", "SoftTech", "900012345-0" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "idservice", "service" },
                values: new object[,]
                {
                    { 1, "Mantenimiento de PC" },
                    { 2, "Instalación de Software" },
                    { 3, "Soporte Técnico Remoto" },
                    { 4, "Consultoría IT" },
                    { 5, "Desarrollo de Software" },
                    { 6, "Seguridad Informática" },
                    { 7, "Hosting y Dominios" },
                    { 8, "Diseño Web" },
                    { 9, "Redes y Telecomunicaciones" },
                    { 10, "Backup y Recuperación" }
                });

            migrationBuilder.InsertData(
                table: "Providers_has_Services",
                columns: new[] { "Country_idcountry", "Providers_idprovider", "Services_idservice", "time_value" },
                values: new object[,]
                {
                    { 1, 1, 1, 100000f },
                    { 2, 1, 2, 150000f },
                    { 3, 2, 3, 200000f },
                    { 4, 3, 4, 250000f },
                    { 5, 4, 5, 300000f },
                    { 6, 5, 6, 350000f },
                    { 7, 6, 7, 400000f },
                    { 8, 7, 8, 450000f },
                    { 9, 8, 9, 500000f },
                    { 10, 9, 10, 550000f }
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
