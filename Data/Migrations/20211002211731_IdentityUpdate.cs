using Microsoft.EntityFrameworkCore.Migrations;

namespace BlinkCash.Data.Migrations
{
    public partial class IdentityUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PreferedName",
                table: "AspNetUsers",
                newName: "PreferredName");

            migrationBuilder.AddColumn<bool>(
                name: "HasPreferredName",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasPreferredName",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "PreferredName",
                table: "AspNetUsers",
                newName: "PreferedName");
        }
    }
}
