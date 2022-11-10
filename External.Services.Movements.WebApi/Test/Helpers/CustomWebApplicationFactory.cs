using External.Services.Movements.WebApi.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace External.Services.Movements.WebApi.Test.Helpers
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        readonly string connectionString = "User ID=postgres;Host=localhost;Port=5432;Database=MovementsDb;Password=1234";
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                //Seeding test data into DB
                services.AddTransient<DataSeeder>();
                services.AddDbContext<TestDbContext>(x => x.UseNpgsql(connectionString));

                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetService<TestDbContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    db.Database.EnsureCreated();

                    try
                    {
                        var service = scope.ServiceProvider.GetService<DataSeeder>();
                        service.SeedAllData();
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                            "database with test messages. Error: {Message}", ex.Message);
                    }
                }
            });
        }
    }
}
