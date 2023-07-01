using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BirdClubManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBirdsUsersEventsComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "JoinDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Highlights",
                table: "Tournaments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationCloseDate",
                table: "Tournaments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Highlights",
                table: "Meetings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationCloseDate",
                table: "Meetings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Highlights",
                table: "FieldTrips",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationCloseDate",
                table: "FieldTrips",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePicture",
                table: "Birds",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JoinDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Highlights",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "RegistrationCloseDate",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "Highlights",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "RegistrationCloseDate",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "Highlights",
                table: "FieldTrips");

            migrationBuilder.DropColumn(
                name: "RegistrationCloseDate",
                table: "FieldTrips");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Birds");
        }
    }
}
