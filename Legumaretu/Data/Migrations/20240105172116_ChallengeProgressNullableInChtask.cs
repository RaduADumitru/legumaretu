using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Legumaretu.Data.Migrations
{
    public partial class ChallengeProgressNullableInChtask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChTasks_ChallengeProgresses_ChallengeProgressId",
                table: "ChTasks");

            migrationBuilder.AlterColumn<int>(
                name: "ChallengeProgressId",
                table: "ChTasks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AlterColumn<int>(
                name: "ChallengeProgressId",
                table: "ChTasks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ChTasks_ChallengeProgresses_ChallengeProgressId",
                table: "ChTasks",
                column: "ChallengeProgressId",
                principalTable: "ChallengeProgresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
