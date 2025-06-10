
using Microsoft.EntityFrameworkCore;
using IGlassAPI.Models;

namespace IGlassAPI.Data
{
    public class ClientLogDbContext : DbContext
    {
        private readonly string _schema;
        public ClientLogDbContext(DbContextOptions<ClientLogDbContext> options, string schema = null)
     : base(options)
        {
            _schema = schema;
        }

        public DbSet<DynamicLog> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (!string.IsNullOrEmpty(_schema))
            {
                modelBuilder.HasDefaultSchema(_schema); 
            }

            modelBuilder.Entity<DynamicLog>(entity =>
            {
                entity.ToTable("Logs");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Timestamp);
                entity.Property(e => e.RawData);
            });
        }

       
    }
}
