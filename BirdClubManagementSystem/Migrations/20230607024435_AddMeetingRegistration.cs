using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BirdClubManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddMeetingRegistration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Fee",
                table: "Meetings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MeetingRegistrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MeetingId = table.Column<int>(type: "int", nullable: false),
                    PaymentReceived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingRegistrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingRegistrations_Meetings_MeetingId",
                        column: x => x.MeetingId,
                        principalTable: "Meetings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeetingRegistrations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeetingRegistrations_MeetingId",
                table: "MeetingRegistrations",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingRegistrations_UserId",
                table: "MeetingRegistrations",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeetingRegistrations");

            migrationBuilder.DropColumn(
                name: "Fee",
                table: "Meetings");
        }
    }
}
