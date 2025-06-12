using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginApi.Migrations
{
    /// <inheritdoc />
    public partial class AddCcUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "ccUsers",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ccUsers",
                newName: "User_id");

            migrationBuilder.AddColumn<string>(
                name: "ApellidoMaterno",
                table: "ccUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApellidoPaterno",
                table: "ccUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IDArea",
                table: "ccUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginAttempt",
                table: "ccUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "ccUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nombres",
                table: "ccUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ccUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TipoUser_id",
                table: "ccUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "fCreate",
                table: "ccUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApellidoMaterno",
                table: "ccUsers");

            migrationBuilder.DropColumn(
                name: "ApellidoPaterno",
                table: "ccUsers");

            migrationBuilder.DropColumn(
                name: "IDArea",
                table: "ccUsers");

            migrationBuilder.DropColumn(
                name: "LastLoginAttempt",
                table: "ccUsers");

            migrationBuilder.DropColumn(
                name: "Login",
                table: "ccUsers");

            migrationBuilder.DropColumn(
                name: "Nombres",
                table: "ccUsers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ccUsers");

            migrationBuilder.DropColumn(
                name: "TipoUser_id",
                table: "ccUsers");

            migrationBuilder.DropColumn(
                name: "fCreate",
                table: "ccUsers");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "ccUsers",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "User_id",
                table: "ccUsers",
                newName: "Id");
        }
    }
}
