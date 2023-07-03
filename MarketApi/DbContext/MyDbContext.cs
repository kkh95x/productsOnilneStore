using MarketApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class MyDbContext:DbContext
    {
       public DbSet<Car> Cars { get; set; }
       public DbSet<Customer> Customers { get; set; }
       public DbSet<Part> Parts { get; set; } 
       public DbSet<Sale> Sales { get; set; }
       public DbSet<Supplier> Suppliers { get; set; }

      public  MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {         

          //to init many to many in car part tables
        // modelBuilder.Entity<Car>()
        //.HasMany(c => c.Parts)
        //.WithMany(p => p.Cars)
        //.UsingEntity<Dictionary<string, object>>(
        //    "CarPart",
        //    j => j
        //        .HasOne<Part>()
        //        .WithMany()
        //        .HasForeignKey("PartId"),
        //    j => j
        //        .HasOne<Car>()
        //        .WithMany()
        //        .HasForeignKey("CarId"),
        //    j =>
        //    {
        //        j.HasKey("CarId", "PartId");
        //        j.HasData(
        //            new { CarId = 1, PartId = 1 }
                
        //        );
        //    }
        //);


            base.OnModelCreating(modelBuilder);
        }

      
    }
}