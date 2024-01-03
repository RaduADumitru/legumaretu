using Legumaretu.Data;
using Legumaretu.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Legumaretu.Pages
{
    public class ChallengesModel : PageModel
    {
        public List<Challenge> challenges = DummyData.TestChallenges;

        public void OnGet()
        {
        }
    }
}
