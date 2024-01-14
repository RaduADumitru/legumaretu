using System.ComponentModel.DataAnnotations;
using Legumaretu.Data;

namespace Legumaretu.Models
{
	public class Challenge
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Numele provocării este obligatoriu!")]
		[StringLength(50)]
		public string Name { get; set; }
		public string? Description { get; set; }
		public bool Official { get; set; }
		public virtual List<Recipe>? Recipes { get; set; }
		public virtual List<ChallengeProgress>? ChallengeProgresses { get; set; }
		public ApplicationUser? User { get; set; }

		public Challenge(int id, string name, string description, bool official, List<Recipe> recipes, List<ChallengeProgress>? challengeProgresses)
		{
			Id = id;
			Name = name;
			Description = description;
			Official = official;
			Recipes = recipes;
			ChallengeProgresses = challengeProgresses;
		}
		public Challenge(int id, string name, string description, bool official, List<Recipe> recipes)
		{
			Id = id;
			Name = name;
			Description = description;
			Official = official;
			Recipes = recipes;
		}

		public Challenge() { }

		public int TotalPoints()
		{
			int s = 0;

			foreach (var recipe in Recipes)
			{
				s += recipe.getPoints();
			}

			return s;
		}

		public int CompletionBonus()
		{
			return TotalPoints() / 4;
		}

		public void Delete(ApplicationDbContext applicationDbContext)
		{
			if (ChallengeProgresses != null)
			{
				foreach (var challengeProgress in ChallengeProgresses)
				{
					challengeProgress.Delete(applicationDbContext);
				}
			}
			applicationDbContext.Challenges.Remove(this);
		}
	}
}
