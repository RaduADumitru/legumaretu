using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Legumaretu.Data;
using Legumaretu.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Legumaretu.Pages
{
	[Authorize(Roles = "Default,Moderator,Admin")]
	public class CreateChallengeModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		private readonly Legumaretu.Data.ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;

		public CreateChallengeModel(ILogger<IndexModel> logger, Legumaretu.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			_logger = logger;
			_context = context;
			_userManager = userManager;
		}

		public async Task<IActionResult> OnGetAsync()
		{
			ApplicationUser user = _userManager.GetUserAsync(User).Result;
			Recipes = _context.Recipes.Where(x => x.Official || x.User.Id == user.Id).OrderBy(x => x.Name).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
			return Page();
		}

		[BindProperty]
		public Challenge Challenge { get; set; } = default!;

		[BindProperty]
		[Required(ErrorMessage = "Selectează cel puțin o rețetă"), MinLength(1)]
		public int[] SelectedRecipeIds { get; set; } = default!; // To store selected recipe IDs
		public List<SelectListItem> Recipes { get; set; }


		// To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid || _context.Challenges == null || Challenge == null)
			{
				return Page();
			}
			// Associate selected recipes with the newly created Challenge
			Challenge.Recipes = new List<Recipe>();
			var selectedRecipes = await _context.Recipes.Where(r => SelectedRecipeIds.Contains(r.Id)).ToListAsync();
			foreach (var recipe in selectedRecipes)
			{
				Challenge.Recipes.Add(recipe);
			}
			ApplicationUser user = _userManager.GetUserAsync(User).Result;
			Challenge.User = user;
			if (User.IsInRole("Admin") || User.IsInRole("Moderator"))
			{
				Challenge.Official = true;
			}
			else
			{
				Challenge.Official = false;
			}
			_context.Challenges.Add(Challenge);
			await _context.SaveChangesAsync();

			return RedirectToPage("./Challenges");
		}
	}
}
