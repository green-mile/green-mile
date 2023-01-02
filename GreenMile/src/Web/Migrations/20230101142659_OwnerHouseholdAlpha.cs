using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    public partial class OwnerHouseholdAlpha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Household",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Household",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Disabled",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Household_OwnerId",
                table: "Household",
                column: "OwnerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Household_AspNetUsers_OwnerId",
                table: "Household",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Household_AspNetUsers_OwnerId",
                table: "Household");

            migrationBuilder.DropIndex(
                name: "IX_Household_OwnerId",
                table: "Household");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Household");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Household");

            migrationBuilder.DropColumn(
                name: "Disabled",
                table: "AspNetUsers");
        }
    }
}
