using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment3.Data;
using Assignment3.Models;

namespace Assignment3.Pages
{
    public class EditModel : PageModel
    {
        private readonly Assignment3.Data.ApplicationDbContext _context;

        public EditModel(Assignment3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CheckIn CheckIn { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.CheckIns == null)
            {
                return NotFound();
            }

            var checkin =  await _context.CheckIns.FirstOrDefaultAsync(m => m.Id == id);
            if (checkin == null)
            {
                return NotFound();
            }
            CheckIn = checkin;
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

            _context.Attach(CheckIn).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CheckInExists(CheckIn.Id))
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

        private bool CheckInExists(int id)
        {
          return _context.CheckIns.Any(e => e.Id == id);
        }
    }
}
