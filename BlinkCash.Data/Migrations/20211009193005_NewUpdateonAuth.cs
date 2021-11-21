using Microsoft.EntityFrameworkCore.Migrations;

namespace BlinkCash.Data.Migrations
{
    public partial class NewUpdateonAuth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasTransactionPin",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "HasRequestedCard",
                table: "Account",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasRequestedCard",
                table: "Account");

            migrationBuilder.AddColumn<bool>(
                name: "HasTransactionPin",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
