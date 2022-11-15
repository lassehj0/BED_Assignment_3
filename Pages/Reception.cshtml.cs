using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment3.Pages
{
    [Authorize("ReceptionOnly")]
    public class ReceptionModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
