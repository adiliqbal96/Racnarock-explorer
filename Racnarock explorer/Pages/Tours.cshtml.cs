using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Racnarock_explorer.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Racnarock_explorer.Pages
{
    public class ToursModel : PageModel
    {
        public List<Tour> Tours { get; set; }

        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("LoggedInUser")))
            {
                return RedirectToPage("/Login");
            }

            Tours = LoadToursFromFile();
            return Page();
        }

        private List<Tour> LoadToursFromFile()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "tours.json");
            var tours = new List<Tour>();

            if (System.IO.File.Exists(filePath))
            {
                var json = System.IO.File.ReadAllText(filePath);
                tours = JsonSerializer.Deserialize<List<Tour>>(json);
            }
            else
            {
                // Predefined tours
                tours = new List<Tour>
                {
                    new Tour { Id = 1, Title = "Rock Historie", Description = "En rejse gennem rockmusikkens historie fra 50'erne til i dag.", AudioUrl = "/Music/rock_historie.mp3" },
                    new Tour { Id = 2, Title = "Punk Revolution", Description = "Oplev punkens vilde dage og dens indflydelse på musik og kultur.", AudioUrl = "/Music/Punk_evoulution.mp3" },
                    new Tour { Id = 3, Title = "Heavy Metal Thunder", Description = "Udforsk heavy metal's opstigning og de legendariske bands, der formede genren.", AudioUrl = "/Music/Heavy_metal.mp3" },
                    new Tour { Id = 4, Title = "Elektroniske Eksperimenter", Description = "Dyk ned i den elektroniske musiks historie og dens futuristiske lyde.", AudioUrl = "/Music/Electronic.mp3" }
                };

                var updatedJson = JsonSerializer.Serialize(tours, new JsonSerializerOptions { WriteIndented = true });
                System.IO.File.WriteAllText(filePath, updatedJson);
            }

            return tours;
        }
    }
}
