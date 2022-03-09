using System.ComponentModel.DataAnnotations;

namespace Music.Models
{
    public class ErrorLogger
    {
        [Key]
        public int LoggerId { get; set; }
        [Required]
        public string ErrorDetails { get; set; } = String.Empty;
        public DateTime LogDate { get; set; } = DateTime.Now;
    }
}
