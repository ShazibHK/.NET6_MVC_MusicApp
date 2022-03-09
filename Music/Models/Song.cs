using System.ComponentModel.DataAnnotations;

namespace Music.Models
{
    public class Song
    {
        [Key]
        public int SongId { get; set; }
        [Required]
        [Display(Name = "Song Name")]
        public string? SongName { get; set; }

        [Display(Name = "Language")]
        [Required]
        public string? language { get; set; }

        [Required]
        [Display(Name = "Duration")]
        public string? duration { get; set; }

        [Required]
        [Display(Name = "Year")]
        public string? Year { get; set; }
        public string? type { get; set; }
        public int? PlayListId { get; set; }
        public string? Image { get; set; }
        public string? SongPath { get; set; }
    }
}
