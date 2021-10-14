using DataTier.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;

namespace DataTier.Context
{
    public class CustomerContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string accountEndpoint = Environment.GetEnvironmentVariable("accountEndpoint");
            string accountKey = Environment.GetEnvironmentVariable("accountKey");
            string databaseName = Environment.GetEnvironmentVariable("databaseName");

            optionsBuilder.UseCosmos(
                accountEndpoint,
                accountKey,
                databaseName);
        }
    }
}
