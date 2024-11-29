using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpotKapasite.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kapasiteler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KapasiteAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LisansNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoktaKodu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KurumAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoktaAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KapasiteMiktari = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Fiyat = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DonemTur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ay = table.Column<int>(type: "int", nullable: false),
                    Yil = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kapasiteler", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kapasiteler");
        }
    }
}
