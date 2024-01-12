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

		public ChallengesModel(ILogger<IndexModel> logger, ApplicationDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		public void OnGet()
		{
			Challenges = _context.Challenges.Include(c => c.Recipes).ToList();
		}
	}
}
