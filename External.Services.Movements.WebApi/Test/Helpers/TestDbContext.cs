using External.Services.Movements.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace External.Services.Movements.WebApi.Test.Helpers
{
    public class TestDbContext : DbContext
    {
        public TestDbContext()
        {
        }
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCustomer> ProductCustomer { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProductCustomer>().HasKey(table => new
            {
                table.ProductId,
                table.CustomerId
            });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("AppDb");
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}
