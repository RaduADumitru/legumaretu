using Legumaretu.Data;

namespace Legumaretu.Models;

public class ChallengeProgress
{
	public int Id { get; set; }
	public ApplicationUser? User { get; set; }
	public int ChallengeId { get; set; }
	public Challenge? Challenge { get; set; }
	public virtual List<ChTask>? ChTasks { get; set; }

	public void Delete(ApplicationDbContext applicationDbContext)
	{
		if (ChTasks != null)
		{
			foreach (var chTask in ChTasks)
			{
				chTask.Delete(applicationDbContext);
			}
		}
		applicationDbContext.ChallengeProgresses.Remove(this);
	}
}