using Legumaretu.Data;
using Legumaretu.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Legumaretu.Pages
{
    public class AddRecipeModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        [BindProperty] public Recipe Recipe { get; set; }
        public AddRecipeModel(ILogger<IndexModel> logger, ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }
        public void OnGet()
        {
            Recipe = new Recipe();
        }

        public IActionResult OnPost()
        {
            // if (!ModelState.IsValid)
            // {
            // 	return Page();
            // }
            ApplicationUser user = _userManager.GetUserAsync(User).Result;
            if (User.IsInRole("Admin") || User.IsInRole("Moderator"))
            {
                Recipe.Official = true;
            }
            else
            {
	            Recipe.Official = false;
            }
            Recipe.User = user;
            _applicationDbContext.Recipes.Add(Recipe);
            _applicationDbContext.SaveChanges();
            return RedirectToPage("/Index");
        }
    }
}
