using Microsoft.AspNetCore.Identity;

namespace Legumaretu.Models
{
    public class ApplicationUser : IdentityUser
	{
		public List<Challenge> Challenges { get; set; }
		public List<Recipe> Recipes { get; set; }
		public List<ChallengeProgress> ChallengeProgresses { get; set; }

		public int getPoints(bool arePointsOfficial)
		{
			int s = 0;

			foreach (var prog in ChallengeProgresses)
			{
				if (arePointsOfficial == prog.Challenge.Official)
				{
					foreach (var chTask in prog.ChTasks)
					{
						if (chTask.Done)
						{
							s += chTask.Recipe.getPoints();
						}
					}

					if (prog.Completed())
					{
						s += prog.Challenge.CompletionBonus();
					}
				}
			}

			return s;
		}
	}
}
