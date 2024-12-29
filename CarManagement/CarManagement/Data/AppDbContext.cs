using Car_Management.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Car_Management.Data
{
    public class AppDbContext : DbContext
    {
         public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

         public DbSet<Garage> Garages { get; set; }
         public DbSet<Car> Cars { get; set; }
         public DbSet<Maintenance> Maintenances { get; set; }

         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            

        }
    }
}
