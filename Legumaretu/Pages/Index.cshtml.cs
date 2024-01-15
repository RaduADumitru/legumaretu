using Legumaretu.Data;
using Legumaretu.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Legumaretu.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		private readonly ApplicationDbContext _context;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly UserManager<ApplicationUser> _userManager;
		public List<Recipe> Recipes { get; set; }

		public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
		{
			_logger = logger;
			_context = context;
			_signInManager = signInManager;
			_userManager = userManager;
		}

		public void OnGet(string searchStr, string starFilter)
		{
			if (!String.IsNullOrEmpty(searchStr))
			{
				searchStr = searchStr.Trim().ToLower();
				Recipes = _context.Recipes.Include(r => r.User).Where(r => r.Name.Trim().ToLower().Contains(searchStr)).ToList();
			}
			else if (!String.IsNullOrEmpty(starFilter))
			{
				Recipes = _context.Recipes.Include(r => r.User).Where(r => r.Stars == int.Parse(starFilter)).ToList();
			}
			else
			{
				Recipes = _context.Recipes.Include(r => r.User).ToList();
			}
			//if not logged in, show only official recipes
			if (!_signInManager.IsSignedIn(User))
			{
				Recipes = Recipes.Where(r => r.Official).ToList();
			}
			else if (User.IsInRole("Moderator") || User.IsInRole("Administrator"))
			{
				//show all recipes
				Recipes = Recipes.ToList();
			}
			else
			{
				//show only official recipes and own recipes
				String userId = _userManager.GetUserId(User);
				Recipes = Recipes.Where(r => r.Official || r.User.Id == userId).ToList();
			}

		}
	}
}