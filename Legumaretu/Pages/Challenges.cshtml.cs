using Legumaretu.Data;
using Legumaretu.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Legumaretu.Pages
{
    public class ChallengesModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;
        public List<Challenge> Challenges { get; set; }
        public int MinPoints { get; set; }
        public int MaxPoints { get; set; }

        public ChallengesModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
            
            MinPoints = _context.Challenges.Include(c => c.Recipes).ToList().Min(c => c.TotalPoints());
            MaxPoints = _context.Challenges.Include(c => c.Recipes).ToList().Max(c => c.TotalPoints());
        }

        public void OnGet(string searchStr, string filterValue)
        {
            if (!String.IsNullOrEmpty(searchStr))
            {
                searchStr = searchStr.Trim().ToLower();
                Challenges = _context.Challenges.Include(c => c.Recipes).Where(c => c.Name.Trim().ToLower().Contains(searchStr)).ToList();
            }
            else if (!String.IsNullOrEmpty(filterValue))
            {
                int value = int.Parse(filterValue);
                Challenges = _context.Challenges.Include(c => c.Recipes).AsEnumerable().Where(c => c.TotalPoints() <= value).ToList();
            }
            else
            {
                Challenges = _context.Challenges.Include(c => c.Recipes).ToList();
            }
        }
    }
}
