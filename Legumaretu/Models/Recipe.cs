using System.ComponentModel.DataAnnotations;
using Legumaretu.Data;

namespace Legumaretu.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Numele rețetei este obligatoriu!")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Descrierea rețetei este obligatorie!")]
        public string Description { get; set; }
        [Range(1, 5, ErrorMessage = "Rating-ul de dificultate trebuie să fie între 1 și 5 stele!")]
        public int Stars { get; set; }
        public bool Official { get; set; }
        public string? ImgLink { get; set; } = "https://cdn.pixabay.com/photo/2016/03/05/19/02/hamburger-1238246_960_720.jpg";
        public List<ChTask>? ChTasks { get; set; }
        public ApplicationUser? User { get; set; }
        public List<Challenge>? Challenges { get; set; }

        public Recipe(int id, string name, string description, int stars, bool official, string imglink)
        {
            Id = id;
            Name = name;
            Description = description;
            Stars = stars;
            Official = official;
            ImgLink = imglink;
        }
        public Recipe() { }

        public int getPoints()
        {
            return Stars * 20;
        }

        public void Delete(ApplicationDbContext applicationDbContext)
        {
	        if (ChTasks != null)
	        {
		        foreach (var chTask in ChTasks)
		        {
					chTask.Delete(applicationDbContext);
     //                // remove the challenge progress associated with the chTask if it has no more tasks
     //                if (chTask.ChallengeProgress.ChTasks.Count == 1)
     //                {
					// 	chTask.ChallengeProgress.Delete(applicationDbContext);
					// }
				}
			}
			applicationDbContext.Recipes.Remove(this);
        }
    }
}
