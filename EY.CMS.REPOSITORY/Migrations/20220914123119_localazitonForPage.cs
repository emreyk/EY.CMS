using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EY.CMS.REPOSITORY.Migrations
{
    public partial class localazitonForPage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameEN",
                table: "Pages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameRU",
                table: "Pages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TextEN",
                table: "Pages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TextRU",
                table: "Pages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameEN",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "NameRU",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "TextEN",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "TextRU",
                table: "Pages");
        }
    }
}
