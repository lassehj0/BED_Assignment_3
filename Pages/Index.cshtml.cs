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
    public class IndexModel : PageModel
    {
        private readonly Assignment3.Data.ApplicationDbContext _context;

        public IndexModel(Assignment3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<CheckIn> CheckIn { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.CheckIns != null)
            {
                CheckIn = await _context.CheckIns.ToListAsync();
            }
        }
    }
}
