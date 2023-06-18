using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BirdClubManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class FixBlogCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Blogs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "Blogs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
