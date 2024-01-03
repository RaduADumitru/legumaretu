using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Legumaretu.Data.Migrations
{
    public partial class UpdateChallengeProgress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChTasks_ChallengeProgresses_ChallengeProgressId",
                table: "ChTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_ChTasks_Challenges_ChallengeId",
                table: "ChTasks");

            migrationBuilder.DropIndex(
                name: "IX_ChTasks_ChallengeId",
                table: "ChTasks");

            migrationBuilder.DropColumn(
                name: "ChallengeId",
                table: "ChTasks");

            migrationBuilder.AddColumn<int>(
                name: "ChallengeId",
                table: "Recipes",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ChallengeProgressId",
                table: "ChTasks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_ChallengeId",
                table: "Recipes",
                column: "ChallengeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChTasks_ChallengeProgresses_ChallengeProgressId",
                table: "ChTasks",
                column: "ChallengeProgressId",
                principalTable: "ChallengeProgresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Challenges_ChallengeId",
                table: "Recipes",
                column: "ChallengeId",
                principalTable: "Challenges",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChTasks_ChallengeProgresses_ChallengeProgressId",
                table: "ChTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Challenges_ChallengeId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_ChallengeId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "ChallengeId",
                table: "Recipes");

            migrationBuilder.AlterColumn<int>(
                name: "ChallengeProgressId",
                table: "ChTasks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ChallengeId",
                table: "ChTasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChTasks_ChallengeId",
                table: "ChTasks",
                column: "ChallengeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChTasks_ChallengeProgresses_ChallengeProgressId",
                table: "ChTasks",
                column: "ChallengeProgressId",
                principalTable: "ChallengeProgresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChTasks_Challenges_ChallengeId",
                table: "ChTasks",
                column: "ChallengeId",
                principalTable: "Challenges",
                principalColumn: "Id");
        }
    }
}
