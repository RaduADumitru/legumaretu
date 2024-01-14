using Legumaretu.Data;
using Legumaretu.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Legumaretu.Pages
{
	[Authorize(Roles = "Admin")]
	public class EditUserRolesModel : PageModel
	{
		[BindProperty(SupportsGet = true)]
		public string UserId { get; set; }

		public List<string> UserRoles { get; set; }
		public List<IdentityRole> AllRoles { get; set; }
		private readonly ILogger<IndexModel> _logger;
		private readonly ApplicationDbContext _applicationDbContext;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public EditUserRolesModel(ILogger<IndexModel> logger, ApplicationDbContext applicationDbContext,
			UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_logger = logger;
			_applicationDbContext = applicationDbContext;
			_userManager = userManager;
			_roleManager = roleManager;
		}
		public async Task<IActionResult> OnGetAsync(string userId)
		{
			var user = await _userManager.FindByIdAsync(userId);
			if (user == null)
			{
				return RedirectToPage("./Error");
			}

			UserId = userId;
			UserRoles = (await _userManager.GetRolesAsync(user)).ToList();
			AllRoles = _roleManager.Roles.ToList();

			return Page();
		}

		public async Task<IActionResult> OnPostAsync(List<string> selectedRoles)
		{
			var user = await _userManager.FindByIdAsync(UserId);
			if (user == null)
			{
				return RedirectToPage("./Error");
			}

			var existingRoles = await _userManager.GetRolesAsync(user);
			var rolesToAdd = selectedRoles.Except(existingRoles);
			var rolesToRemove = existingRoles.Except(selectedRoles);

			await _userManager.AddToRolesAsync(user, rolesToAdd);
			await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

			return RedirectToPage("/Index");
		}
	}
}
