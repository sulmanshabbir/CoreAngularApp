using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreAngularApp.Migrations
{
    public partial class reviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerReviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ReviewDate = table.Column<DateTime>(nullable: false),
                    Subject = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Rating = table.Column<int>(nullable: false),
                    Ansi = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerReviews", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerReviews");
        }
    }
}
