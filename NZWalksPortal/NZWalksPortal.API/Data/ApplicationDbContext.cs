using Microsoft.EntityFrameworkCore;
using NZWalksPortal.API.Models.Domain;

namespace NZWalksPortal.API.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Walk> Walk { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<Difficulty> Difficultie { get; set; }
    }
}
