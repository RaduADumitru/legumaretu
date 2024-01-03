using Legumaretu.Data;
using Legumaretu.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Legumaretu.Pages
{
    public class RecipeModel : PageModel
    {
        public Recipe Recipe { get; set; }

        public void OnGet(int recipeId)
        {
            Recipe = DummyData.TestRecipes.Find(recipe => recipe.Id == recipeId);
        }
    }
}
