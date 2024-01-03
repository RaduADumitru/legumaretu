namespace Legumaretu.Models
{
	public class ChTask
	{
		public int Id { get; set; }
		public Recipe Recipe { get; set; }
		public bool Done { get; set; }

		public ChTask(int id, Recipe recipe)
		{
			Id = id;
			Recipe = recipe;
			Done = false;
		}

		public ChTask(int id, Recipe recipe, bool done)
		{
			Id = id;
			Recipe = recipe;
			Done = done;
		}

		public ChTask() { }
	}
}
