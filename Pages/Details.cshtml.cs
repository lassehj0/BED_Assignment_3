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

      public CheckIn CheckIn { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.CheckIns == null)
            {
                return NotFound();
            }

            var checkin = await _context.CheckIns.FirstOrDefaultAsync(m => m.Id == id);
            if (checkin == null)
            {
                return NotFound();
            }
            else 
            {
                CheckIn = checkin;
            }
            return Page();
        }
    }
}
