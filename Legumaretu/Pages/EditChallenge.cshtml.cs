﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Legumaretu.Data;
using Legumaretu.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Legumaretu.Pages
{
    [Authorize(Roles = "Default,Moderator,Admin")]
    public class EditChallengeModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Legumaretu.Data.ApplicationDbContext _context;

        public EditChallengeModel(ILogger<IndexModel> logger, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public Challenge Challenge { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Challenges == null)
            {
                return NotFound();
            }

            var challenge =  await _context.Challenges.Include(x => x.User).FirstOrDefaultAsync(m => m.Id == id);
            if (challenge == null)
            {
                return NotFound();
            }
            Challenge = challenge;
            // Default users can only edit their own challenges
            if (!User.IsInRole("Admin") && !User.IsInRole("Moderator"))
            {
                ApplicationUser user = _userManager.GetUserAsync(User).Result;
                if (user == null || challenge.User.Id != user.Id)
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

            _context.Attach(Challenge).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChallengeExists(Challenge.Id))
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

        private bool ChallengeExists(int id)
        {
          return (_context.Challenges?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
