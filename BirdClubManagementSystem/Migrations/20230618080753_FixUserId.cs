using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BirdClubManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class FixUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Users_UserID",
                table: "Blogs");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Blogs",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Blogs_UserID",
                table: "Blogs",
                newName: "IX_Blogs_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Users_UserId",
                table: "Blogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Users_UserId",
                table: "Blogs");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Blogs",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Blogs_UserId",
                table: "Blogs",
                newName: "IX_Blogs_UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Users_UserID",
                table: "Blogs",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
