using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using IGlassAPI.Models;

namespace IGlassAPI.Data
{
    public class LogsDbContext : DbContext
    {
        public LogsDbContext(DbContextOptions<LogsDbContext> options) : base(options) {}

        public DbSet<LogEntry> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogEntry>().ToTable("Logs");
        }
    }

    public class LogsDbContextFactory
    {
        private readonly IConfiguration _config;

        public LogsDbContextFactory(IConfiguration config)
        {
            _config = config;
        }

        public LogsDbContext Create(string clientId)
        {
            var schema = _config.GetSection("ClientSchemas")[clientId];
            var connectionString = _config.GetConnectionString("Postgres");
            var options = new DbContextOptionsBuilder<LogsDbContext>()
                .UseNpgsql($"{connectionString};Search Path={schema}")
                .Options;
            return new LogsDbContext(options);
        }
    }
}