using Microsoft.EntityFrameworkCore;

namespace EFMemoryExample.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
       base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
