namespace Legumaretu.Models
{
	public class Challenge
	{
		public int Id {  get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public bool Official {  get; set; }
		//TODO: replace chtasks with recipes
		public List<ChTask> ChTasks { get; set; }
		public ApplicationUser User { get; set; }

		public Challenge(int id, string name, string description, bool official, List<ChTask> chtasks)
		{
			Id = id;
			Name = name;
			Description = description;
			Official = official;
			ChTasks = chtasks;
		}

		public Challenge(){}
		public int TotalPoints()
		{
			int s = 0;

			foreach(var task in ChTasks)
			{
				s += task.Recipe.Stars * 20;
			}

			return s;
		}
	}
}
