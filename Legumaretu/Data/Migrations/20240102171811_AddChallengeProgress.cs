using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Legumaretu.Data.Migrations
{
    public partial class AddChallengeProgress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChallengeProgressId",
                table: "ChTasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ChallengeProgresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ChallengeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChallengeProgresses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChallengeProgresses_Challenges_ChallengeId",
                        column: x => x.ChallengeId,
                        principalTable: "Challenges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChTasks_ChallengeProgressId",
                table: "ChTasks",
                column: "ChallengeProgressId");

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeProgresses_ChallengeId",
                table: "ChallengeProgresses",
                column: "ChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeProgresses_UserId",
                table: "ChallengeProgresses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChTasks_ChallengeProgresses_ChallengeProgressId",
                table: "ChTasks",
                column: "ChallengeProgressId",
                principalTable: "ChallengeProgresses",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChTasks_ChallengeProgresses_ChallengeProgressId",
                table: "ChTasks");

            migrationBuilder.DropTable(
                name: "ChallengeProgresses");

            migrationBuilder.DropIndex(
                name: "IX_ChTasks_ChallengeProgressId",
                table: "ChTasks");

            migrationBuilder.DropColumn(
                name: "ChallengeProgressId",
                table: "ChTasks");
        }
    }
}
