using System.ComponentModel.DataAnnotations;

namespace Music.Models
{
    public class PlayList
    {
        [Key]
        public int PlayListId { get; set; }
        [Required]
        public string? PlayListName { get; set; }
        [Required]
        public string? type { get; set; }
        public string? Image { get; set; }
    }
}
