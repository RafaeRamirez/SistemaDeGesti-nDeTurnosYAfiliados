using System.ComponentModel.DataAnnotations;

namespace systemdeeps.WebApplication.Models
{
    // Turn (ticket) entity for queueing system
    public class Turn
    {
        public int Id { get; set; }

        // Sequential number for the day
        public int Number { get; set; }

        // "Pending" | "Attending" | "Completed"
        [Required, MaxLength(20)]
        public string Status { get; set; } = "Pending";

        // Optional description
        [MaxLength(250)]
        public string? Description { get; set; }

        // Creation datetime
        public DateTime DateTime { get; set; } = DateTime.Now;

        // Foreign key to Affiliate
        public int AffiliateId { get; set; }
        public Affiliate? Affiliate { get; set; }
    }
}