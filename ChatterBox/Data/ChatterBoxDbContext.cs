using ChatterBox.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatterBox.Data
{
    public class ChatterBoxDbContext : DbContext
    {
        public ChatterBoxDbContext(DbContextOptions<ChatterBoxDbContext> options)
            : base(options)
        {
        }

        public DbSet<ChatInteraction> ChatMessages { get; set; }
    }
}
