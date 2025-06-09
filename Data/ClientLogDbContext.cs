
using Microsoft.EntityFrameworkCore;
using IGlassAPI.Models;

namespace IGlassAPI.Data
{
    public class ClientLogDbContext : DbContext
    {
        public ClientLogDbContext(DbContextOptions<ClientLogDbContext> options) : base(options) { }

        public DbSet<LogEntry> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogEntry>().ToTable("Logs");
        }
    }
}
