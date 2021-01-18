using Microsoft.EntityFrameworkCore.Migrations;

namespace TAK.Data.Migrations
{
    public partial class AddLatinNameProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LatinName",
                table: "Locations",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LatinName",
                table: "Locations");
        }
    }
}
