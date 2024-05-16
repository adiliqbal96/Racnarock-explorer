using System.ComponentModel.DataAnnotations;

namespace Racnarock_explorer.Models
{
    public class Tour
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "AudioUrl is required")]
        public string AudioUrl { get; set; }
    }
}
