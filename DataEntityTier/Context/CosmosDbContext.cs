using DataTier.Models;
using Microsoft.EntityFrameworkCore;

namespace DataTier.Context
{
    public class CosmosDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public CosmosDbContext(DbContextOptions<CosmosDbContext> contextOptions)
            : base(contextOptions)
        {
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultContainer("Customers");

            modelBuilder.Entity<Customer>()
                .ToContainer("Customers");

            modelBuilder.Entity<Customer>()
                .HasNoDiscriminator();

            modelBuilder.Entity<Customer>()
                .Property(c => c.CustomerId)
                .ToJsonProperty("id")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Customer>()
                .HasKey(c => c.CustomerId);

            modelBuilder.Entity<Customer>()
                .HasPartitionKey(c => c.Lastname);

            modelBuilder.Entity<Customer>()
                .UseETagConcurrency();

            modelBuilder.Entity<Customer>()
                .OwnsOne(c => c.MortgageOffer)
                .WithOwner(m => m.Customer);
        }
    }
}
