using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Assignment3.Data;
using Assignment3.Models;

namespace Assignment3.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly Assignment3.Data.ApplicationDbContext _context;

        public DeleteModel(Assignment3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public TotalBookingsPerDay TotalBookingsPerDay { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TotalBookingsPerDay == null)
            {
                return NotFound();
            }

            var totalbookingsperday = await _context.TotalBookingsPerDay.FirstOrDefaultAsync(m => m.Id == id);

            if (totalbookingsperday == null)
            {
                return NotFound();
            }
            else 
            {
                TotalBookingsPerDay = totalbookingsperday;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.TotalBookingsPerDay == null)
            {
                return NotFound();
            }
            var totalbookingsperday = await _context.TotalBookingsPerDay.FindAsync(id);

            if (totalbookingsperday != null)
            {
                TotalBookingsPerDay = totalbookingsperday;
                _context.TotalBookingsPerDay.Remove(TotalBookingsPerDay);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
