using Microsoft.EntityFrameworkCore.Migrations;

namespace TAK.Data.Migrations
{
    public partial class AddLatinTitleProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LatinTitle",
                table: "NewsPosts",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LatinTitle",
                table: "NewsPosts");
        }
    }
}
