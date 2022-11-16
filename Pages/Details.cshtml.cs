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
    public class DetailsModel : PageModel
    {
        private readonly Assignment3.Data.ApplicationDbContext _context;

        public DetailsModel(Assignment3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
