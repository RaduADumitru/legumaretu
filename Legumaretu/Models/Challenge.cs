using System.ComponentModel.DataAnnotations;

namespace Legumaretu.Models
{
	public class Challenge
	{
		public int Id {  get; set; }
		[Required]
        [StringLength(50)]
        public string Name { get; set; }
		public string? Description { get; set; }
		public bool Official {  get; set; }
		public List<Recipe>? Recipes { get; set; }
		public ApplicationUser? User { get; set; }

		public Challenge(int id, string name, string description, bool official, List<Recipe> recipes)
		{
			Id = id;
			Name = name;
			Description = description;
			Official = official;
			Recipes = recipes;
		}

		public Challenge(){}
		public int TotalPoints()
		{
			int s = 0;

			foreach(var recipe in Recipes)
			{
				s += recipe.Stars * 20;
			}

			return s;
		}
	}
}
