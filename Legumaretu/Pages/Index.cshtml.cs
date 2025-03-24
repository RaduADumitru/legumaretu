using Legumaretu.Data;
using Legumaretu.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Legumaretu.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
		private readonly ApplicationDbContext _context;
		public List<Recipe> Recipes { get; set; }

		public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
		{
			_logger = logger;
			_context = context;
		}

        public void OnGet()
		{
			Recipes = _context.Recipes.ToList();
		}
	}
}