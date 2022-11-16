using Assignment3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Assignment3.DTO;

namespace Assignment3.Pages
{
	public class KitchenModel : PageModel
	{
		private readonly Assignment3.Data.ApplicationDbContext _context;

		public KitchenModel(Assignment3.Data.ApplicationDbContext context)
		{
			_context = context;
		}

		public TotalBookingsPerDay_Kitchen_DTO CheckIn { get; set; } = new TotalBookingsPerDay_Kitchen_DTO();


		[BindProperty]
		public DateTime Day { get; set; }

        public IActionResult Process(DateTime day)
        {
			Day = day.Date;

            if (_context.CheckIns != null && _context.TotalBookingsPerDay != null)
            {
				int expectedAdultGuests = _context.TotalBookingsPerDay
					.Where(c => c.Date == Day)
					.Select(c => c.TotalAdults)
                    .FirstOrDefault();
				int expectedChildGuests = _context.TotalBookingsPerDay
                    .Where(c => c.Date == Day)
                    .Select(c => c.TotalChildren)
					.FirstOrDefault();
				int totalExpectedGuests = expectedAdultGuests + expectedChildGuests;

				int adultsCheckedIn = _context.CheckIns
					.Where(c => c.Date == Day)
					.Select(c => c.Adults)
					.Sum();

                int childrenCheckedIn = _context.CheckIns
                    .Where(c => c.Date == Day)
                    .Select(c => c.Children)
                    .Sum();

				int notCheckedInTotal = totalExpectedGuests - (adultsCheckedIn + childrenCheckedIn);
				int notCheckedInAdults = expectedAdultGuests - adultsCheckedIn;
				int notCheckedInChildren = expectedChildGuests - childrenCheckedIn;

				CheckIn.TotalAdults = expectedAdultGuests;
				CheckIn.TotalChildren = expectedChildGuests;
				CheckIn.TotalGuests = totalExpectedGuests;
				CheckIn.CheckedInAdults = adultsCheckedIn;
				CheckIn.CheckedInChildren = childrenCheckedIn;
				CheckIn.RemainingGuests = notCheckedInTotal;
				CheckIn.RemainingAdults = notCheckedInAdults;
				CheckIn.RemainingChildren = notCheckedInChildren;
            }

			return Page();
        }

        public async Task OnGetAsync()
		{
			Process(DateTime.Today.Date);
		}
    }
}
