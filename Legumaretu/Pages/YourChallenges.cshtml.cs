using Legumaretu.Data;
using Legumaretu.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Legumaretu.Pages
{
    public class YourChallengesModel : PageModel
    {
        private readonly ILogger<YourChallengesModel> _logger;
        private readonly ApplicationDbContext _context;
        public List<ChallengeProgress> Progresses { get; set; }

        public YourChallengesModel(ILogger<YourChallengesModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            Progresses = _context.ChallengeProgresses.Include(p => p.Challenge).Include(p => p.ChTasks).ToList();
        }
    }
}
