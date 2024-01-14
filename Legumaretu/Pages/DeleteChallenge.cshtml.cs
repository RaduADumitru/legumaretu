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
	public class DeleteChallengeModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly Legumaretu.Data.ApplicationDbContext _context;

		public DeleteChallengeModel(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
		{
			_context = context;
			_userManager = userManager;
		}

		[BindProperty]
		public Challenge Challenge { get; set; } = default!;

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null || _context.Challenges == null)
			{
				return RedirectToPage("./Error");
			}

			var challenge = await _context.Challenges.Include(x => x.User).FirstOrDefaultAsync(m => m.Id == id);

			if (challenge == null)
			{
				return RedirectToPage("./Error");
			}
			else
			{
				// default users can only delete their own challenges
				if (!User.IsInRole("Admin") && !User.IsInRole("Moderator"))
				{
					ApplicationUser user = _userManager.GetUserAsync(User).Result;
					if (user == null || challenge.User.Id != user.Id)
					{
						return RedirectToPage("./Error");
					}
				}
				Challenge = challenge;
			}
			return Page();
		}

		public async Task<IActionResult> OnPostAsync(int? id)
		{
			if (id == null || _context.Challenges == null)
			{
				return RedirectToPage("./Error");
			}
			var challenge = await _context.Challenges.Include(x => x.ChallengeProgresses).ThenInclude(x => x.ChTasks).FirstOrDefaultAsync(m => m.Id == id);

			if (challenge != null)
			{
				Challenge = challenge;
				Challenge.Delete(_context);
				await _context.SaveChangesAsync();
			}

			return RedirectToPage("./Index");
		}
	}
}
