using BulkyWeb.Models.DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BulkyWeb.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }

        //THIS IS METHOD IS DEFAULT IN EFC (just overwriting with our own code)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category{ ID = Guid.NewGuid(), CategoryName = "Action", CategoryOrder = 1 }
               //new Category{ ID = Guid.NewGuid(), CategoryName = "Horror", CategoryOrder = 2 },
                );
            //base.OnModelCreating(modelBuilder);
        }
    }
}
