using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Legumaretu.Data;
using Legumaretu.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Legumaretu.Pages
{
    [Authorize(Roles = "Default,Moderator,Admin")]
    public class SignUpForChallengeModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly Legumaretu.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public List<SelectListItem> Challenges { get; set; }


        public SignUpForChallengeModel(ILogger<IndexModel> logger, Legumaretu.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            ApplicationUser user = _userManager.GetUserAsync(User).Result;
            Challenges = _context.Challenges.Where(x => x.Official || x.User.Id == user.Id).OrderBy(x => x.Name).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            return Page();
        }

        [BindProperty]
        public ChallengeProgress ChallengeProgress { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.ChallengeProgresses == null || ChallengeProgress == null)
            {
                return Page();
            }
            ApplicationUser user = _userManager.GetUserAsync(User).Result;
            ChallengeProgress.User = user;
            // Associate selected recipes with the newly created Challenge Progress
            List<ChTask> tasks = new List<ChTask>();
            Challenge challenge = _context.Challenges.Include(x => x.Recipes).FirstOrDefault(x => x.Id == ChallengeProgress.ChallengeId);
            foreach (var recipe in challenge.Recipes)
            {
                ChTask task = new ChTask(recipe);
                _context.ChTasks.Add(task);
                tasks.Add(task);
            }
            ChallengeProgress.ChTasks = tasks;
            _context.ChallengeProgresses.Add(ChallengeProgress);
            await _context.SaveChangesAsync();

            return RedirectToPage("./YourChallenges");
        }
    }
}
