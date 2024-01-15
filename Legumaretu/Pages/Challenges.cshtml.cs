using Legumaretu.Data;
using Legumaretu.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Legumaretu.Pages
{
    public class ChallengesModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public List<Challenge> Challenges { get; set; }
        public int MinPoints { get; set; }
        public int MaxPoints { get; set; }

        public ChallengesModel(ILogger<IndexModel> logger, ApplicationDbContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _signInManager = signInManager;
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
                Challenges = _context.Challenges.Include(c => c.User).Include(c => c.Recipes).Where(c => c.Name.Trim().ToLower().Contains(searchStr)).ToList();
            }
            else if (!String.IsNullOrEmpty(filterValue))
            {
                int value = int.Parse(filterValue);
                Challenges = _context.Challenges.Include(c => c.User).Include(c => c.Recipes).AsEnumerable().Where(c => c.TotalPoints() <= value).ToList();
            }
            else
            {
                Challenges = _context.Challenges.Include(c => c.User).Include(c => c.Recipes).ToList();
            }
            //if not logged in, show only official challenges
            if (!_signInManager.IsSignedIn(User))
            {
	            Challenges = Challenges.Where(c => c.Official).ToList();
            }
            else if (User.IsInRole("Moderator") || User.IsInRole("Administrator"))
            {
	            //show all recipes
	            Challenges = Challenges.ToList();
            }
            else
            {
	            //show only official recipes and own recipes
	            String userId = _userManager.GetUserId(User);
	            Challenges = Challenges.Where(c => c.Official || c.User.Id == userId).ToList();
            }
		}
    }
}
