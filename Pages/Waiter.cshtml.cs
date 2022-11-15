using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment3.Pages
{
    [Authorize("WaiterOnly")]
    public class WaiterModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
