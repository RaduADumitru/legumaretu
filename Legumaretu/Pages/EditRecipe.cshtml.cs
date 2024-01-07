using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Legumaretu.Data;
using Legumaretu.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Legumaretu.Pages
{
    [Authorize(Roles = "Default,Moderator,Admin")]
    public class EditRecipeModel : PageModel
    {
        [BindProperty]
        public IFormFile? Image { get; set; }
        private readonly string webRoot = "wwwroot";
        private readonly ILogger<IndexModel> _logger;
        private readonly Legumaretu.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditRecipeModel(ILogger<IndexModel> logger, Legumaretu.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Recipe Recipe { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Recipes == null)
            {
                return NotFound();
            }

            var recipe =  await _context.Recipes.Include(x => x.User).FirstOrDefaultAsync(m => m.Id == id);
            if (recipe == null)
            {
                return NotFound();
            }
            Recipe = recipe;
            // Default users can only edit their own recipes
            if (!User.IsInRole("Admin") && !User.IsInRole("Moderator"))
            {
                ApplicationUser user = _userManager.GetUserAsync(User).Result;
                if (user == null || recipe.User.Id != user.Id)
                {
                    return NotFound();
                }
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Image != null && Image.Length > 0)
            {
	            // Upload image to wwwroot/content/images/recipes
	            //Get file extension
	            string fileExtension = Path.GetExtension(Image.FileName);
	            var fileName = Guid.NewGuid().ToString() + fileExtension;
	            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\content\images\recipes", fileName);
	            using (var fileStream = new FileStream(filePath, FileMode.Create))
	            {
		            Image.CopyTo(fileStream);
	            }
	            Recipe.ImgLink = "/content/images/recipes/" + fileName;
            }
            _context.Attach(Recipe).State = EntityState.Modified;
			try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(Recipe.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToPage("./Index");
        }

        private bool RecipeExists(int id)
        {
          return (_context.Recipes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
