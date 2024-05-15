using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Racnarock_explorer.Models;
using System.Collections.Generic;

namespace Racnarock_explorer.Pages
{
    public class ToursModel : PageModel
    {
        public List<Tour> Tours { get; set; }

        public IActionResult OnGet()
        {
            // Check if the user is logged in by verifying the session
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("LoggedInUser")))
            {
                // Redirect to the login page if the user is not logged in
                return RedirectToPage("/Login");
            }

            // User is logged in, proceed with loading the tours
            Tours = new List<Tour>
            {
                new Tour { Id = 1, Title = "Rock Historie", Description = "En rejse gennem rockmusikkens historie fra 50'erne til i dag." , AudioUrl = "/music/rock_historie.mp3" },
                new Tour { Id = 2, Title = "Punk Revolution", Description = "Oplev punkens vilde dage og dens indflydelse på musik og kultur.", AudioUrl = "/music/Punk_evoulution.mp3" },
                new Tour { Id = 3, Title = "Heavy Metal Thunder", Description = "Udforsk heavy metal's opstigning og de legendariske bands, der formede genren.", AudioUrl = "music/Heavy_metal.mp3" },
                new Tour { Id = 4, Title = "Elektroniske Eksperimenter", Description = "Dyk ned i den elektroniske musiks historie og dens futuristiske lyde." },
                new Tour { Id = 5, Title = "Folkets Musik", Description = "En fortælling om folkemusikkens rolle og udvikling gennem tiderne." },
                new Tour { Id = 6, Title = "Jazzens Rejse", Description = "Følg jazzens udvikling fra New Orleans' barer til verdens store scener." },
                new Tour { Id = 7, Title = "Pop Ikonernes Æra", Description = "En gennemgang af popmusikkens mest indflydelsesrige figurer og hvordan de formede moderne popkultur." }
            };

            return Page();
        }
    }
}
