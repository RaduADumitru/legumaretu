using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Legumaretu.Data;
using Legumaretu.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Legumaretu.Pages
{
	[Authorize(Roles = "Default,Moderator,Admin")]
	public class DeleteRecipeModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly Legumaretu.Data.ApplicationDbContext _context;

		public DeleteRecipeModel(UserManager<ApplicationUser> userManager, Legumaretu.Data.ApplicationDbContext context)
		{
			_userManager = userManager;
			_context = context;
		}

		[BindProperty]
		public Recipe Recipe { get; set; } = default!;

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null || _context.Recipes == null)
			{
				return RedirectToPage("./Error");
			}

			var recipe = await _context.Recipes.Include(x => x.User).FirstOrDefaultAsync(m => m.Id == id);

			if (recipe == null)
			{
				return RedirectToPage("./Error");
			}
			else
			{
				// default users can only delete their own recipes
				if (!User.IsInRole("Admin") && !User.IsInRole("Moderator"))
				{
					ApplicationUser user = _userManager.GetUserAsync(User).Result;
					if (user == null || recipe.User.Id != user.Id)
					{
						return RedirectToPage("./Error");
					}
				}
				Recipe = recipe;
			}
			return Page();
		}

		public async Task<IActionResult> OnPostAsync(int? id)
		{
			if (id == null || _context.Recipes == null)
			{
				return RedirectToPage("./Error");
			}
			var recipe = await _context.Recipes.Include(x => x.ChTasks).FirstOrDefaultAsync(m => m.Id == id);

			if (recipe != null)
			{
				Recipe = recipe;
				Recipe.Delete(_context);
				await _context.SaveChangesAsync();
			}

			return RedirectToPage("./Index");
		}
	}
}
