using Microsoft.EntityFrameworkCore.Migrations;

namespace ArtGalleryAPI.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArtWork",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Artist = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: false),
                    Genre = table.Column<string>(nullable: true),
                    Review = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    ImgPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtWork", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtWork");
        }
    }
}
