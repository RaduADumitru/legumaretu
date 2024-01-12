using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Legumaretu.Data;
using Legumaretu.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Legumaretu.Pages
{
	[Authorize(Roles = "Default,Moderator,Admin")]
	public class EditChallengeProgressModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		private readonly Legumaretu.Data.ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;

		public EditChallengeProgressModel(ILogger<IndexModel> logger, Legumaretu.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			_logger = logger;
			_context = context;
			_userManager = userManager;
		}

		[BindProperty]
		public ChallengeProgress ChallengeProgress { get; set; } = default!;
		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null || _context.ChallengeProgresses == null)
			{
				return RedirectToPage("./Error");
			}

			var challengeprogress = await _context.ChallengeProgresses.Include(x => x.User).Include(x => x.ChTasks).ThenInclude(x => x.Recipe).FirstOrDefaultAsync(m => m.Id == id);
			if (challengeprogress == null)
			{
				return RedirectToPage("./Error");
			}
			ChallengeProgress = challengeprogress;
			// Default users can only edit their own challenge progresses
			if (!User.IsInRole("Admin") && !User.IsInRole("Moderator"))
			{
				ApplicationUser user = _userManager.GetUserAsync(User).Result;
				if (user == null || challengeprogress.User.Id != user.Id)
				{
					return RedirectToPage("./Error");
				}
			}
			return Page();
		}

		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see https://aka.ms/RazorPagesCRUD.
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}
			foreach (var task in ChallengeProgress.ChTasks)
			{
				// so that changes for ChTasks are checked
				_context.Attach(task).State = EntityState.Modified;
			}

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ChallengeProgressExists(ChallengeProgress.Id))
				{
					return RedirectToPage("./Error");
				}
				else
				{
					throw;
				}
			}

			return RedirectToPage("./YourChallenges");
		}

		private bool ChallengeProgressExists(int id)
		{
			return (_context.ChallengeProgresses?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
