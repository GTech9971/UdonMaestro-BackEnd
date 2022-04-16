using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UdonMaestro_BackEnd.Migrations
{
    public partial class addShopTypeRegularHolidayupdateShop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Lat = table.Column<decimal>(type: "numeric(7,4)", nullable: false),
                    Lon = table.Column<decimal>(type: "numeric(7,4)", nullable: false),
                    PostCode = table.Column<string>(type: "text", nullable: false),
                    TownId = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "ShopTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ShopTypeId = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    AddressId = table.Column<int>(type: "integer", nullable: false),
                    Tel = table.Column<string>(type: "text", nullable: false),
                    ExistsParking = table.Column<bool>(type: "boolean", nullable: false),
                    ExistsCoinParking = table.Column<bool>(type: "boolean", nullable: false),
                    EnableTakeout = table.Column<bool>(type: "boolean", nullable: false),
                    Memo = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shops_Address_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shops_ShopTypes_ShopTypeId",
                        column: x => x.ShopTypeId,
                        principalTable: "ShopTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegularHolidays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ShopId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegularHolidays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegularHolidays_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id");
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

            migrationBuilder.CreateIndex(
                name: "IX_RegularHolidays_Name",
                table: "RegularHolidays",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_RegularHolidays_ShopId",
                table: "RegularHolidays",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_Shops_AddressId",
                table: "Shops",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Shops_Name",
                table: "Shops",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Shops_ShopTypeId",
                table: "Shops",
                column: "ShopTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopTypes_Name",
                table: "ShopTypes",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegularHolidays");

            migrationBuilder.DropTable(
                name: "Shops");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "ShopTypes");
        }
    }
}
