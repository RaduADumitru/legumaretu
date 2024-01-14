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

        public void OnGet(string searchStr, string starFilter)
		{
			if (!String.IsNullOrEmpty(searchStr))
			{
				searchStr = searchStr.Trim().ToLower();
				Recipes = _context.Recipes.Where(r => r.Name.Trim().ToLower().Contains(searchStr)).ToList();
			}
			else if (!String.IsNullOrEmpty(starFilter)) 
			{
                Recipes = _context.Recipes.Where(r => r.Stars == int.Parse(starFilter)).ToList();
            }
			else
			{
				Recipes = _context.Recipes.ToList();
			}
        }
	}
}