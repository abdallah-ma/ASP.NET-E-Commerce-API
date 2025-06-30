using Microsoft.EntityFrameworkCore;
using OrderService.Models;
using System.Reflection;
using DemoAPI.Common;

namespace OrderService
{
    public class AppDbContext : BaseDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.Entity<Order>().OwnsOne(o => o.ShippingAddress);

            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }


        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }


    }
}
