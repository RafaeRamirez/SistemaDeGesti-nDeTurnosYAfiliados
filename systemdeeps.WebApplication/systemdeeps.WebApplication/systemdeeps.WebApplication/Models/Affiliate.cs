using System.ComponentModel.DataAnnotations;

namespace systemdeeps.WebApplication.Models
{
    // Affiliate entity: holds user profile info
    public class Affiliate
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string FullName { get; set; } = string.Empty;

        [Required, MaxLength(25)]
        public string DocumentNumber { get; set; } = string.Empty;

        [EmailAddress, MaxLength(150)]
        public string? Email { get; set; }

        [MaxLength(30)]
        public string? Phone { get; set; }

        public DateTime DateRegistered { get; set; } = DateTime.Now;

        // Navigation: one affiliate → many turns
        public ICollection<Turn>? Turns { get; set; }
    }
}