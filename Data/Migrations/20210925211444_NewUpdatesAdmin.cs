using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlinkCash.Data.Migrations
{
    public partial class NewUpdatesAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "UserSecurityQuestionAndAnswer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "UserSecurityQuestionAndAnswer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "UserSecurityQuestionAndAnswer",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserSecurityQuestionAndAnswer",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "UserSecurityQuestionAndAnswer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "UserSecurityQuestionAndAnswer",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecordStatus",
                table: "UserSecurityQuestionAndAnswer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasTransactionPin",
                table: "Account",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTransactionPinHashed",
                table: "Account",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TransactionPin",
                table: "Account",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "UpdateFlag",
                table: "Account",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Answer",
                table: "UserSecurityQuestionAndAnswer");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "UserSecurityQuestionAndAnswer");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "UserSecurityQuestionAndAnswer");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserSecurityQuestionAndAnswer");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "UserSecurityQuestionAndAnswer");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "UserSecurityQuestionAndAnswer");

            migrationBuilder.DropColumn(
                name: "RecordStatus",
                table: "UserSecurityQuestionAndAnswer");

            migrationBuilder.DropColumn(
                name: "HasTransactionPin",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "IsTransactionPinHashed",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "TransactionPin",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "UpdateFlag",
                table: "Account");
        }
    }
}
