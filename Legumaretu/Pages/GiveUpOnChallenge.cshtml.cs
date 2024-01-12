using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Legumaretu.Data;
using Legumaretu.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Legumaretu.Pages
{
	[Authorize(Roles = "Default,Moderator,Admin")]
	public class GiveUpOnChallengeModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly Legumaretu.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GiveUpOnChallengeModel(ILogger<IndexModel> logger, Legumaretu.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
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

            var challengeprogress = await _context.ChallengeProgresses.Include(x => x.Challenge).FirstOrDefaultAsync(m => m.Id == id);

            if (challengeprogress == null)
            {
                return RedirectToPage("./Error");
			}
            else 
            {
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
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.ChallengeProgresses == null)
            {
                return RedirectToPage("./Error");
			}
            var challengeprogress = await _context.ChallengeProgresses.Include(x => x.ChTasks).FirstOrDefaultAsync(m => m.Id == id);

            if (challengeprogress != null)
            {
                ChallengeProgress = challengeprogress;
                // remove each task in the challengeprogress
                foreach (var task in ChallengeProgress.ChTasks)
                {
                    _context.ChTasks.Remove(task);
                }
                _context.ChallengeProgresses.Remove(ChallengeProgress);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./YourChallenges");
        }
    }
}
