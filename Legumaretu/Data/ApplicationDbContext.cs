using Legumaretu.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Legumaretu.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public DbSet<Challenge> Challenges { get; set; }
		public DbSet<Recipe> Recipes { get; set; }
		public DbSet<ChTask> ChTasks { get; set; }
		public DbSet<ChallengeProgress> ChallengeProgresses { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

	}
}