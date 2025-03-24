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

	public ChallengeProgress(int id, int challengeId, Challenge challenge, List<ChTask> chTasks)
	{
		Id = id; 
		ChallengeId = challengeId;
		Challenge = challenge; 
		ChTasks = chTasks;
	}

	public ChallengeProgress() {  }

	public bool Completed()
	{
		foreach(var chTask in ChTasks)
		{
			if (!chTask.Done)
				return false;
		}

		return true;
	}
}