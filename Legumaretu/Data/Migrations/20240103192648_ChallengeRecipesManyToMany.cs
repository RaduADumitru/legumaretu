using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Legumaretu.Data.Migrations
{
    public partial class ChallengeRecipesManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Challenges_ChallengeId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_ChallengeId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "ChallengeId",
                table: "Recipes");

            migrationBuilder.CreateTable(
                name: "ChallengeRecipe",
                columns: table => new
                {
                    ChallengesId = table.Column<int>(type: "int", nullable: false),
                    RecipesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeRecipe", x => new { x.ChallengesId, x.RecipesId });
                    table.ForeignKey(
                        name: "FK_ChallengeRecipe_Challenges_ChallengesId",
                        column: x => x.ChallengesId,
                        principalTable: "Challenges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChallengeRecipe_Recipes_RecipesId",
                        column: x => x.RecipesId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeRecipe_RecipesId",
                table: "ChallengeRecipe",
                column: "RecipesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChallengeRecipe");

            migrationBuilder.AddColumn<int>(
                name: "ChallengeId",
                table: "Recipes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_ChallengeId",
                table: "Recipes",
                column: "ChallengeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Challenges_ChallengeId",
                table: "Recipes",
                column: "ChallengeId",
                principalTable: "Challenges",
                principalColumn: "Id");
        }
    }
}
