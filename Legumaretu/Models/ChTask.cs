namespace Legumaretu.Models
{
	public class ChTask
	{
		public int Id { get; set; }
		public Recipe Recipe { get; set; }
		public bool Done { get; set; }
		public ChallengeProgress ChallengeProgress { get; set; }

		public ChTask(int id, Recipe recipe)
		{
			Id = id;
			Recipe = recipe;
			Done = false;
		}

		public ChTask(int id, Recipe recipe, bool done, ChallengeProgress challengeProgress)
		{
			Id = id;
			Recipe = recipe;
			Done = done;
			ChallengeProgress = challengeProgress;
		}

        public ChTask(Recipe recipe)
        {
            Recipe = recipe;
            Done = false;
        }

		public ChTask() { }
	}
}
