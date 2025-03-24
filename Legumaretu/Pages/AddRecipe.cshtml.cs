using Legumaretu.Data;
using Legumaretu.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Net.Http.Headers;

namespace Legumaretu.Pages
{
    [Authorize(Roles = "Default,Moderator,Admin")]
    public class AddRecipeModel : PageModel
    {
        [BindProperty]
        public IFormFile? Image { get; set; }
        private readonly string webRoot = "wwwroot";
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
            if (!ModelState.IsValid)
            {
            	return Page();
            }
            ApplicationUser user = _userManager.GetUserAsync(User).Result;
            Recipe.User = user;
			if (User.IsInRole("Admin") || User.IsInRole("Moderator"))
            {
                Recipe.Official = true;
            }
            else
            {
	            Recipe.Official = false;
            }

			if (Image != null && Image.Length > 0)
			{
				// Upload image to wwwroot/content/images/recipes
                // Get file extension
                string fileExtension = Path.GetExtension(Image.FileName);
				var fileName = Guid.NewGuid().ToString() + fileExtension;
				var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\content\images\recipes", fileName);
				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					Image.CopyTo(fileStream);
				}
                Recipe.ImgLink = "/content/images/recipes/" + fileName;
			}
			_applicationDbContext.Recipes.Add(Recipe);
            _applicationDbContext.SaveChanges();
            return RedirectToPage("/Index");
        }
    }
}
