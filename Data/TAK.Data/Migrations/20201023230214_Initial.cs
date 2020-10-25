namespace TAK.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: false),
                    Adress = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true),
                    FacebookPage = table.Column<string>(nullable: true),
                    InstagramPage = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NewsPosts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(nullable: false),
                    Content = table.Column<string>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: false),
                    VideoUrl = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsPosts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocationPictures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    LocationId = table.Column<int>(nullable: false),
                    PictureUrl = table.Column<string>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationPictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationPictures_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NewsPostPictures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    NewsPostId = table.Column<int>(nullable: false),
                    PictureUrl = table.Column<string>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsPostPictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsPostPictures_NewsPosts_NewsPostId",
                        column: x => x.NewsPostId,
                        principalTable: "NewsPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocationPictures_IsDeleted",
                table: "LocationPictures",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_LocationPictures_LocationId",
                table: "LocationPictures",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_IsDeleted",
                table: "Locations",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_UserId",
                table: "Locations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsPostPictures_IsDeleted",
                table: "NewsPostPictures",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_NewsPostPictures_NewsPostId",
                table: "NewsPostPictures",
                column: "NewsPostId");

            migrationBuilder.CreateIndex(
                name: "IX_NewsPosts_IsDeleted",
                table: "NewsPosts",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_NewsPosts_UserId",
                table: "NewsPosts",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationPictures");

            migrationBuilder.DropTable(
                name: "NewsPostPictures");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "NewsPosts");
        }
    }
}
