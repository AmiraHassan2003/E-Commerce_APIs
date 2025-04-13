using E_Commerce_APIs.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_APIs.Models.Context
{
    public class E_CommerceDbContext: DbContext
    {
        public E_CommerceDbContext()
        {
            
        }

        public E_CommerceDbContext(DbContextOptions<E_CommerceDbContext> options):base(options)
        {
            
        }

        public DbSet <Category> categories { get; set; }
        public DbSet <Product> products { get; set; }
        public DbSet <Customer> customers { get; set; }
        public DbSet <Admin> admins { get; set; }
        public DbSet <CarRental> cars { get; set; }
        public DbSet <CarIncludeProduct> carIncludeProducts { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasMany(c => c.products).WithOne(p => p.category).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CarRental>().HasOne(c => c.customer).WithOne().OnDelete(DeleteBehavior.Cascade);
            //modelBuilder.Entity<CarIncludeProduct>().HasMany(c => c.car);
            //modelBuilder.Entity<CarIncludeProduct>().HasMany(c => c.product);
            modelBuilder.Entity<CarIncludeProduct>()
            .HasKey(cip => new { cip.CarId, cip.ProductId }); // Composite Key for CarIncludeProduct

            modelBuilder.Entity<CarIncludeProduct>()
                .HasOne(cip => cip.car)
                .WithMany()
                .HasForeignKey(cip => cip.CarId); // Foreign Key for Car

            modelBuilder.Entity<CarIncludeProduct>()
                .HasOne(cip => cip.product)
                .WithMany()
                .HasForeignKey(cip => cip.ProductId); // Fore
        }

    }
}
