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

		public DbSet<Legumaretu.Models.Challenge> Challenge { get; set; }
		public DbSet<Legumaretu.Models.Recipe> Recipe { get; set; }
		public DbSet<Legumaretu.Models.ChTask> ChTask { get; set; }
	}
}