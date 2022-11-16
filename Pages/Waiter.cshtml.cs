using Assignment3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Assignment3.Controllers;
using Assignment3.DTO;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Assignment3.Data;

namespace Assignment3.Pages
{
    [Authorize("WaiterOnly")]
    public class WaiterModel : PageModel
    {
        private readonly Assignment3.Data.ApplicationDbContext _context;

        public WaiterModel(Assignment3.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CheckIn CheckIn { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            CheckIn.Date = DateTime.Now;
            _context.CheckIns.Add(CheckIn);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
