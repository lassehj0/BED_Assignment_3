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
    //[Authorize("WaiterOnly")]
    public class WaiterModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public WaiterModel(ApplicationDbContext context)
        {
            _context = context;
            TypeAdapterConfig<List<CheckIn>, List<CheckInsDTO>>.NewConfig();
            TypeAdapterConfig<CheckInsDTO, CheckIn>.NewConfig();
        }
        public void OnGet()
        {
            
        }
        
    }
}
