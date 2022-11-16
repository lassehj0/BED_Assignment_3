using Assignment3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Mapster;
using Assignment3.DTO;
using Microsoft.AspNetCore.SignalR.Client;

namespace Assignment3.Pages
{
    [Authorize("ReceptionOnly")]
    public class ReceptionModel : PageModel
    {
        private readonly Assignment3.Data.ApplicationDbContext _context;
        private HubConnection _hubConnection = new HubConnectionBuilder()
            .WithUrl(new Uri("https://localhost:7257/DataHub"))
            .WithAutomaticReconnect()
            .Build();

        public ReceptionModel(Assignment3.Data.ApplicationDbContext context)
        {
            _context = context;
            TypeAdapterConfig<TotalBookingsPerDay_Reception_DTO, TotalBookingsPerDay>.NewConfig().IgnoreNullValues(true);

        }

        public IList<CheckIn> CheckIn { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.CheckIns != null)
            {
                CheckIn = await _context.CheckIns.Where(c => c.Date.Date == DateTime.Today.Date).ToListAsync();
            }
        }

        [BindProperty]
        public TotalBookingsPerDay TBookings { get; set; }



        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.TotalBookingsPerDay.Add(TBookings);
            await _context.SaveChangesAsync();
            await _hubConnection.StartAsync();
            await _hubConnection.InvokeAsync("ReceiveMessage");


            return RedirectToPage("./Index");
        }


   
    }
}

