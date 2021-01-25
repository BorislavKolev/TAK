namespace TAK.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddAuthorPropToNewsPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "NewsPosts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "NewsPosts");
        }
    }
}
