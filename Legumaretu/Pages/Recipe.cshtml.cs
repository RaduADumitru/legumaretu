using Legumaretu.Data;
using Legumaretu.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Legumaretu.Pages
{
    public class RecipeModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public Recipe Recipe { get; set; }

        public RecipeModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet(int recipeId)
        {
            Recipe = _context.Recipes.FirstOrDefault(recipe => recipe.Id == recipeId);
        }
    }
}
