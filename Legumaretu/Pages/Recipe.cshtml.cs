using Legumaretu.Data;
using Legumaretu.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Legumaretu.Pages
{
    public class RecipeModel : PageModel
    {
        private readonly ILogger<RecipeModel> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public Recipe Recipe { get; set; }

        public RecipeModel(ILogger<RecipeModel> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGetAsync(int? recipeId)
        {
            if (recipeId == null || _context.Recipes == null)
            {
                return RedirectToPage("./Error");
            }

            var recipe = await _context.Recipes.Include(x => x.User).FirstOrDefaultAsync(m => m.Id == recipeId);
            if (recipe == null)
            {
                return RedirectToPage("./Error");
            }
            Recipe = recipe;
            // Default users can only view their own recipes or official recipes
            if (!recipe.Official)
            {
                if (_signInManager.IsSignedIn(User))
                {
                    if (!User.IsInRole("Admin") && !User.IsInRole("Moderator"))
                    {
                        ApplicationUser user = _userManager.GetUserAsync(User).Result;
                        if (user == null || recipe.User.Id != user.Id)
                        {
                            return RedirectToPage("./Error");
                        }
                    }
                }
                else
                {
                    return RedirectToPage("./Error");
                }
            }
            return Page();
        }
    }
}
