namespace Legumaretu.Models;

public class ChallengeProgress
{
	public int Id { get; set; }
	public ApplicationUser? User { get; set; }
	public int ChallengeId { get; set; }
	public Challenge? Challenge { get; set; }
	public List<ChTask>? ChTasks { get; set; }
}