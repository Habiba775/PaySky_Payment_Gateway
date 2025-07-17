using Microsoft.EntityFrameworkCore;
using Domain.Entities; // Assuming Merchant and others are here

namespace InfraStructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Merchant> Merchants { get; set; }
    }
}

