using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Restaurant.Backend.Entities.Entities;

namespace Restaurant.Backend.Entities.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<IdentificationType> IdentificationTypes { get; set; }

        public DbSet<Customer> Customers { get; set; }

    }
}
