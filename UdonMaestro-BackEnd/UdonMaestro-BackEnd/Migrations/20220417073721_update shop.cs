using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UdonMaestro_BackEnd.Migrations
{
    public partial class updateshop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shops_Address_AddressId",
                table: "Shops");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Shops",
                newName: "TownId");

            migrationBuilder.RenameIndex(
                name: "IX_Shops_AddressId",
                table: "Shops",
                newName: "IX_Shops_TownId");

            migrationBuilder.AddColumn<string>(
                name: "AddressLine",
                table: "Shops",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Lat",
                table: "Shops",
                type: "numeric(7,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Lon",
                table: "Shops",
                type: "numeric(7,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PostCode",
                table: "Shops",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_Towns_TownId",
                table: "Shops",
                column: "TownId",
                principalTable: "Towns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shops_Towns_TownId",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "AddressLine",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "Lat",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "Lon",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "PostCode",
                table: "Shops");

            migrationBuilder.RenameColumn(
                name: "TownId",
                table: "Shops",
                newName: "AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Shops_TownId",
                table: "Shops",
                newName: "IX_Shops_AddressId");

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TownId = table.Column<int>(type: "integer", nullable: false),
                    Lat = table.Column<decimal>(type: "numeric(7,4)", nullable: false),
                    Lon = table.Column<decimal>(type: "numeric(7,4)", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PostCode = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_Towns_TownId",
                        column: x => x.TownId,
                        principalTable: "Towns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_Lat",
                table: "Address",
                column: "Lat");

            migrationBuilder.CreateIndex(
                name: "IX_Address_Lon",
                table: "Address",
                column: "Lon");

            migrationBuilder.CreateIndex(
                name: "IX_Address_Name",
                table: "Address",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Address_TownId",
                table: "Address",
                column: "TownId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_Address_AddressId",
                table: "Shops",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
