using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EY.CMS.REPOSITORY.Migrations
{
    public partial class localazitonForProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductNameEN",
                table: "Products",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductNameRU",
                table: "Products",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TextEN",
                table: "Products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TextRU",
                table: "Products",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductNameEN",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductNameRU",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TextEN",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TextRU",
                table: "Products");
        }
    }
}
