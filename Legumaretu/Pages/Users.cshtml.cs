using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Legumaretu.Data;
using Legumaretu.Models;
using Microsoft.AspNetCore.Authorization;

namespace Legumaretu.Pages
{
    [Authorize(Roles = "Moderator,Admin")]
    public class UsersModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public UsersModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ApplicationUser> Users { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Users != null)
            {
                Users = _context.Users
	                .Include(u => u.Recipes)
	                .Include(u => u.Challenges).ThenInclude(c => c.Recipes)
	                .Include(u => u.ChallengeProgresses).ThenInclude(chprog => chprog.ChTasks).ThenInclude(chtask => chtask.Recipe)
	                .Include(u => u.ChallengeProgresses).ThenInclude(chprog => chprog.Challenge)
	                .ToList();
            }
        }
    }
}