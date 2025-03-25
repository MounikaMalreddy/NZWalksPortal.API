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
        public DbSet<Difficulty> Difficulty { get; set; }
        public DbSet<Image> Image { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    //Seed data for difficulties
        //    // Easy, Medium, Hard
        //    var difficulties = new List<Difficulty>()
        //    {
        //        new Difficulty() 
        //        {
        //            Id=Guid.Parse("7a777cae-e3c2-422a-9c49-cc7e5bf691e8"),
        //            Name="Easy"
        //        },
        //         new Difficulty()
        //        {
        //            Id=Guid.Parse("86c7d93c-ce50-4ea2-9136-ec027cc5bce9"),
        //            Name="Medium"
        //        },
        //          new Difficulty()
        //        {
        //            Id=Guid.Parse("b796555d-08e4-4341-a6c4-0db3428b137d"),
        //            Name="Hard"
        //        }
        //    };
        //    //seed difficulties to the database
        //    modelBuilder.Entity<Difficulty>().HasData(difficulties);

        //}
    }
}
