using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UdonMaestro_BackEnd.Migrations
{
    public partial class addpostcode2town : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PostCode",
                table: "Towns",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostCode",
                table: "Towns");
        }
    }
}
