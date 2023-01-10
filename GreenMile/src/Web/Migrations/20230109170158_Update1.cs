using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    public partial class Update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_CustomFoods_CustomFoodId",
                table: "Donations");

            migrationBuilder.DropTable(
                name: "CustomFoods");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Donations");

            migrationBuilder.RenameColumn(
                name: "CustomFoodId",
                table: "Donations",
                newName: "FoodItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Donations_CustomFoodId",
                table: "Donations",
                newName: "IX_Donations_FoodItemId");

            migrationBuilder.AddColumn<bool>(
                name: "IsCustom",
                table: "FoodItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Donations",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_FoodItems_FoodItemId",
                table: "Donations",
                column: "FoodItemId",
                principalTable: "FoodItems",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_FoodItems_FoodItemId",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "IsCustom",
                table: "FoodItems");

            migrationBuilder.RenameColumn(
                name: "FoodItemId",
                table: "Donations",
                newName: "CustomFoodId");

            migrationBuilder.RenameIndex(
                name: "IX_Donations_FoodItemId",
                table: "Donations",
                newName: "IX_Donations_CustomFoodId");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Donations",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Donations",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CustomFoods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    ExpiredDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Image = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomFoods", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_CustomFoods_CustomFoodId",
                table: "Donations",
                column: "CustomFoodId",
                principalTable: "CustomFoods",
                principalColumn: "Id");
        }
    }
}
