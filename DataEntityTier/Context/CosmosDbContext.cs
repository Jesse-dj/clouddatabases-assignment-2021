using DataTier.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataTier.Context
{
    public class CosmosDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = Environment.GetEnvironmentVariable("connectionString");
            string databaseName = Environment.GetEnvironmentVariable("databaseName");

            optionsBuilder.UseCosmos(connectionString, databaseName);

            /*optionsBuilder.LogTo(Console.WriteLine);*/
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.HasDefaultContainer("Customers");

            modelBuilder.Entity<Customer>().ToContainer("Customers");

            modelBuilder.Entity<Customer>()
                .HasNoDiscriminator();

            modelBuilder.Entity<Customer>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Customer>()
                .HasPartitionKey(c => c.partitionKey);

            modelBuilder.Entity<Customer>()
                .UseETagConcurrency();

            modelBuilder.Entity<Customer>()
                .OwnsOne(typeof(MortgageOffer), "MortgageOffer");
        }
    }
}
