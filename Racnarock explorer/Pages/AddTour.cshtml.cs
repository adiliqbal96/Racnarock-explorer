using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Racnarock_explorer.Models;
using Racnarock_explorer.Services;  // Make sure this line is correct
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Racnarock_explorer.Pages
{
    public class AddTourModel : PageModel
    {
        private readonly ILogger<AddTourModel> _logger;
        private readonly FileUploadService _fileUploadService;

        public AddTourModel(ILogger<AddTourModel> logger, FileUploadService fileUploadService)
        {
            _logger = logger;
            _fileUploadService = fileUploadService;
        }

        [BindProperty]
        public Tour Tour { get; set; }

        [BindProperty]
        public IFormFile AudioFile { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (AudioFile == null || AudioFile.Length == 0)
            {
                ModelState.AddModelError("AudioFile", "Please select a file to upload.");
                return Page();
            }

            try
            {
                Tour.AudioUrl = await _fileUploadService.UploadFileAsync(AudioFile, Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Music"));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error uploading file: {ex.Message}");
                ModelState.AddModelError("", "Error uploading file.");
                return Page();
            }

            if (!TryValidateModel(Tour, nameof(Tour)))
            {
                _logger.LogError("Model state is invalid after setting AudioUrl");
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    foreach (var error in state.Errors)
                    {
                        _logger.LogError($"Error in {key}: {error.ErrorMessage}");
                    }
                }
                return Page();
            }

            SaveTour(Tour);
            TempData["SuccessMessage"] = "Tour added successfully!";
            return RedirectToPage("/Tours");
        }

        private void SaveTour(Tour tour)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "tours.json");
            List<Tour> tours = new List<Tour>();
            if (System.IO.File.Exists(filePath))
            {
                var json = System.IO.File.ReadAllText(filePath);
                tours = JsonSerializer.Deserialize<List<Tour>>(json);
            }

            tour.Id = tours.Count > 0 ? tours[^1].Id + 1 : 1;
            tours.Add(tour);

            var updatedJson = JsonSerializer.Serialize(tours, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(filePath, updatedJson);
        }
    }
}
