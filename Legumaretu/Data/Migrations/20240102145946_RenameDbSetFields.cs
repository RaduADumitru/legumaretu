using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Legumaretu.Data.Migrations
{
    public partial class RenameDbSetFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Challenge_AspNetUsers_UserId",
                table: "Challenge");

            migrationBuilder.DropForeignKey(
                name: "FK_ChTask_Challenge_ChallengeId",
                table: "ChTask");

            migrationBuilder.DropForeignKey(
                name: "FK_ChTask_Recipe_RecipeId",
                table: "ChTask");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_AspNetUsers_UserId",
                table: "Recipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipe",
                table: "Recipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChTask",
                table: "ChTask");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Challenge",
                table: "Challenge");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "Recipe",
                newName: "Recipes");

            migrationBuilder.RenameTable(
                name: "ChTask",
                newName: "ChTasks");

            migrationBuilder.RenameTable(
                name: "Challenge",
                newName: "Challenges");

            migrationBuilder.RenameIndex(
                name: "IX_Recipe_UserId",
                table: "Recipes",
                newName: "IX_Recipes_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ChTask_RecipeId",
                table: "ChTasks",
                newName: "IX_ChTasks_RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_ChTask_ChallengeId",
                table: "ChTasks",
                newName: "IX_ChTasks_ChallengeId");

            migrationBuilder.RenameIndex(
                name: "IX_Challenge_UserId",
                table: "Challenges",
                newName: "IX_Challenges_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Recipes",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Challenges",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipes",
                table: "Recipes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChTasks",
                table: "ChTasks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Challenges",
                table: "Challenges",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Challenges_AspNetUsers_UserId",
                table: "Challenges",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChTasks_Challenges_ChallengeId",
                table: "ChTasks",
                column: "ChallengeId",
                principalTable: "Challenges",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChTasks_Recipes_RecipeId",
                table: "ChTasks",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_AspNetUsers_UserId",
                table: "Recipes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Challenges_AspNetUsers_UserId",
                table: "Challenges");

            migrationBuilder.DropForeignKey(
                name: "FK_ChTasks_Challenges_ChallengeId",
                table: "ChTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_ChTasks_Recipes_RecipeId",
                table: "ChTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_AspNetUsers_UserId",
                table: "Recipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipes",
                table: "Recipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChTasks",
                table: "ChTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Challenges",
                table: "Challenges");

            migrationBuilder.RenameTable(
                name: "Recipes",
                newName: "Recipe");

            migrationBuilder.RenameTable(
                name: "ChTasks",
                newName: "ChTask");

            migrationBuilder.RenameTable(
                name: "Challenges",
                newName: "Challenge");

            migrationBuilder.RenameIndex(
                name: "IX_Recipes_UserId",
                table: "Recipe",
                newName: "IX_Recipe_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ChTasks_RecipeId",
                table: "ChTask",
                newName: "IX_ChTask_RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_ChTasks_ChallengeId",
                table: "ChTask",
                newName: "IX_ChTask_ChallengeId");

            migrationBuilder.RenameIndex(
                name: "IX_Challenges_UserId",
                table: "Challenge",
                newName: "IX_Challenge_UserId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Recipe",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Challenge",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipe",
                table: "Recipe",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChTask",
                table: "ChTask",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Challenge",
                table: "Challenge",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Challenge_AspNetUsers_UserId",
                table: "Challenge",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChTask_Challenge_ChallengeId",
                table: "ChTask",
                column: "ChallengeId",
                principalTable: "Challenge",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChTask_Recipe_RecipeId",
                table: "ChTask",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipe_AspNetUsers_UserId",
                table: "Recipe",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
