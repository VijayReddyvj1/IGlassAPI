using IGlassAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace IGlassAPI.Data
{
    public class MasterDbContext : DbContext
    {
        public MasterDbContext(DbContextOptions<MasterDbContext> options) : base(options) { }

        public DbSet<ClientLogMaster> ClientLogMasters { get; set; }
    }
}
