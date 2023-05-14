using Bulky.Models.DomainModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Bulky.DataAcess.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        //THIS IS METHOD IS DEFAULT IN EFC (just overwriting with our own code)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Seeding data
            modelBuilder.Entity<Category>().HasData(
                new Category { ID = Guid.NewGuid(), CategoryName = "Action", CategoryOrder = 1 }
                //new Category{ ID = Guid.NewGuid(), CategoryName = "Horror", CategoryOrder = 2 },
                );

            //modelBuilder.Entity<Product>().HasData(
            //    new Product
            //    {
            //        ID = Guid.NewGuid(),
            //        Title = "Test",
            //        Description = "This is description",
            //        Author = "Nabin Gurung",
            //        ISBN = "ISBN",
            //        ListPrice = 450,
            //        Price = 500,
            //        Price50 = 400,
            //        Price100 = 300,
            //        CategoryID = Guid.Parse("9D0A3B5A-067A-45C7-87D4-23E5D87EACC0"),
            //    }
            //    );
        }
    }
}
