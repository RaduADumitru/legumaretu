using Legumaretu.Data;
using Legumaretu.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Legumaretu.Pages
{
    public class YourChallengesModel : PageModel
    {
        private readonly ILogger<YourChallengesModel> _logger;
        private readonly ApplicationDbContext _context;
        public List<ChallengeProgress> Progresses { get; set; }
        public int MinPoints { get; set; }
        public int MaxPoints { get; set; }

        public YourChallengesModel(ILogger<YourChallengesModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;

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

        public void OnGet(string searchStr, string filterValue, string sortOrder)
        {
            if (!String.IsNullOrEmpty(searchStr))
            {
                searchStr = searchStr.Trim().ToLower();
                Progresses = _context.ChallengeProgresses.Include(p => p.Challenge).Include(p => p.ChTasks).Include(p => p.Challenge.Recipes).Where(c => c.Challenge.Name.Trim().ToLower().Contains(searchStr)).ToList();
            }
            else if (!String.IsNullOrEmpty(filterValue)) {
                int value = int.Parse(filterValue);
                Progresses = _context.ChallengeProgresses.Include(p => p.Challenge).Include(p => p.ChTasks).Include(p => p.Challenge.Recipes).AsEnumerable().Where(c => c.Challenge.TotalPoints() <= value).ToList();
            }
            else
            {
                Progresses = _context.ChallengeProgresses.Include(p => p.Challenge).Include(p => p.ChTasks).Include(p => p.Challenge.Recipes).ToList();
            }

			if (sortOrder == "desc")
			{
				Progresses = Progresses.OrderByDescending(p => p.Challenge.Name).ToList();
			}
			else if (sortOrder == "asc")
			{
				Progresses = Progresses.OrderBy(p => p.Challenge.Name).ToList();
			}
		}
    }
}
