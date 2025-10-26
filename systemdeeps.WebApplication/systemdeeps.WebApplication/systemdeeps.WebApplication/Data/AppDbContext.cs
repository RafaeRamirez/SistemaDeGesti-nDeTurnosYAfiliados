using Microsoft.EntityFrameworkCore;
using systemdeeps.WebApplication.Models;

namespace systemdeeps.WebApplication.Data
{
    // Database context: maps entities and relationships
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Tables
        public DbSet<Affiliate> Affiliates { get; set; }
        public DbSet<Turn> Turns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // One Affiliate -> Many Turns
            modelBuilder.Entity<Turn>()
                .HasOne(t => t.Affiliate)              // each Turn has one Affiliate
                .WithMany(a => a.Turns)                // an Affiliate has many Turns
                .HasForeignKey(t => t.AffiliateId)     // FK in Turns table
                .OnDelete(DeleteBehavior.Cascade);     // delete turns if affiliate is deleted
        }
    }
}