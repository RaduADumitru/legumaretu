using Legumaretu.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Legumaretu.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<Legumaretu.Models.Challenge> Challenges { get; set; }
		public DbSet<Legumaretu.Models.Recipe> Recipes { get; set; }
		public DbSet<Legumaretu.Models.ChTask> ChTasks { get; set; }
		public DbSet<Legumaretu.Models.ChallengeProgress> ChallengeProgresses { get; set; }
	}
}