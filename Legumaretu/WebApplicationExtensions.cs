using Microsoft.AspNetCore.Identity;

namespace Legumaretu;

public static class WebApplicationExtensions
{
	public static async Task<WebApplication> CreateRolesAsync(this WebApplication app, IConfiguration configuration)
	{
		using var scope = app.Services.CreateScope();
		var roleManager = (RoleManager<IdentityRole>)scope.ServiceProvider.GetService(typeof(RoleManager<IdentityRole>));
		var userManager = (UserManager<IdentityUser>)scope.ServiceProvider.GetService(typeof(UserManager<IdentityUser>));
		var roles = configuration.GetSection("Roles").Get<List<string>>();

		foreach (var role in roles)
		{
			if (!await roleManager.RoleExistsAsync(role))
				await roleManager.CreateAsync(new IdentityRole(role));
		}
		// find the user with the admin email 
		var _user = await userManager.FindByEmailAsync("admin@email.com");

		// check if the user exists
		if (_user == null)
		{
			//Here you could create the super admin who will maintain the web app
			var poweruser = new IdentityUser
			{
				UserName = "admin@email.com",
				Email = "admin@email.com",
			};
			string adminPassword = "P@ssw0rd";

			var createPowerUser = await userManager.CreateAsync(poweruser, adminPassword);
			if (createPowerUser.Succeeded)
			{
				//here we tie the new user to the role
				await userManager.AddToRoleAsync(poweruser, "Admin");

			}
		}
		return app;
	}
}