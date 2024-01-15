using Legumaretu.Data;
using Legumaretu.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Legumaretu.Pages
{
	[Authorize(Roles = "Default,Moderator,Admin")]
	public class YourChallengesModel : PageModel
    {
        private readonly ILogger<YourChallengesModel> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public List<ChallengeProgress> Progresses { get; set; }
        public int MinPoints { get; set; }
        public int MaxPoints { get; set; }

        public YourChallengesModel(ILogger<YourChallengesModel> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;

            if (_context.Challenges.Include(c => c.Recipes).ToList().Count() != 0)
            {
                MinPoints = _context.Challenges.Include(c => c.Recipes).ToList().Min(c => c.TotalPoints());
                MaxPoints = _context.Challenges.Include(c => c.Recipes).ToList().Max(c => c.TotalPoints());
            }
            else
            {
                MinPoints = 0;
                MaxPoints = 0;
            }
        }

        public void OnGet(string searchStr, string filterValue)
        {
            if (!String.IsNullOrEmpty(searchStr))
            {
                searchStr = searchStr.Trim().ToLower();
                Progresses = _context.ChallengeProgresses.Include(p => p.User).Include(p => p.Challenge).Include(p => p.ChTasks).Include(p => p.Challenge.Recipes).Where(c => c.Challenge.Name.Trim().ToLower().Contains(searchStr)).ToList();
            }
            else if (!String.IsNullOrEmpty(filterValue)) {
                int value = int.Parse(filterValue);
                Progresses = _context.ChallengeProgresses.Include(p => p.User).Include(p => p.Challenge).Include(p => p.ChTasks).Include(p => p.Challenge.Recipes).AsEnumerable().Where(c => c.Challenge.TotalPoints() <= value).ToList();
            }
            else
            {
                Progresses = _context.ChallengeProgresses.Include(p => p.User).Include(p => p.Challenge).Include(p => p.ChTasks).Include(p => p.Challenge.Recipes).ToList();
            }
            // show only your own challenge progresses
            String userId = _userManager.GetUserId(User);
            Progresses = Progresses.Where(p => p.User.Id == userId).ToList();
        }
    }
}
