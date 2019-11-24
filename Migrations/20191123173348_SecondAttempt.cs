using Microsoft.EntityFrameworkCore.Migrations;

namespace Assignment4.Migrations
{
    public partial class SecondAttempt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Automobiles",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    model = table.Column<string>(nullable: true),
                    model_year = table.Column<string>(nullable: true),
                    engine_size = table.Column<string>(nullable: true),
                    engine_cylinder_count = table.Column<int>(nullable: false),
                    manufacturer_name = table.Column<string>(nullable: true),
                    category_name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Automobiles", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Automobiles");
        }
    }
}
