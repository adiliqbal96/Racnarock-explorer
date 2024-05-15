using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Racnarock_explorer.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            // Check if the user is logged in by checking the session
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("LoggedInUser")))
            {
                // Redirect to the login page if not logged in
                return RedirectToPage("/Login");
            }

            // User is logged in, proceed with page load
            return Page();
        }
    }
}
