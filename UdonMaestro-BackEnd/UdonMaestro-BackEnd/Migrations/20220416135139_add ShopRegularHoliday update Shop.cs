using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UdonMaestro_BackEnd.Migrations
{
    public partial class addShopRegularHolidayupdateShop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegularHolidays_Shops_ShopId",
                table: "RegularHolidays");

            migrationBuilder.DropIndex(
                name: "IX_RegularHolidays_ShopId",
                table: "RegularHolidays");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "RegularHolidays");

            migrationBuilder.CreateTable(
                name: "ShopRegularHoliday",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShopId = table.Column<int>(type: "integer", nullable: false),
                    RegularHolidayId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopRegularHoliday", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopRegularHoliday_RegularHolidays_RegularHolidayId",
                        column: x => x.RegularHolidayId,
                        principalTable: "RegularHolidays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopRegularHoliday_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShopRegularHoliday_RegularHolidayId",
                table: "ShopRegularHoliday",
                column: "RegularHolidayId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopRegularHoliday_ShopId",
                table: "ShopRegularHoliday",
                column: "ShopId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShopRegularHoliday");

            migrationBuilder.AddColumn<int>(
                name: "ShopId",
                table: "RegularHolidays",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegularHolidays_ShopId",
                table: "RegularHolidays",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_RegularHolidays_Shops_ShopId",
                table: "RegularHolidays",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id");
        }
    }
}
