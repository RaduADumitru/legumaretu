using Microsoft.AspNetCore.Identity;

namespace Legumaretu.Models
{
	public class ApplicationUser : IdentityUser
	{
		public List<Challenge> Challenges { get; set; }
		public List<Recipe> Recipes { get; set; }
		public List<ChallengeProgress> ChallengeProgresses { get; set; }
	}
}
