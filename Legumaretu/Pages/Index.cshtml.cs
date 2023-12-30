using Legumaretu.Data;
using Legumaretu.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Legumaretu.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;

		public IndexModel(ILogger<IndexModel> logger)
		{
			_logger = logger;
		}

		public List<Recipe> recipes = DummyData.TestRecipes;

        public void OnGet()
		{
			
		}
	}
}