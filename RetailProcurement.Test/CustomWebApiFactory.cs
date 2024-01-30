using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RetailProcurement.WebAPI.Persistence;
using System.Data.Common;

namespace RetailProcurement.UnitTests;
public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<RetailProcurementDbContext>));

            services.Remove(dbContextDescriptor!);

            var dbConnectionDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbConnection));

            services.Remove(dbConnectionDescriptor!);

            // Create open SqliteConnection so EF won't automatically close it.
            //services.AddSingleton<DbConnection>(container =>
            //{
            //    var connection = new DbConnection("DataSource=:memory:");
            //    connection.Open();

            //    return connection;
            //});

            services.AddDbContext<RetailProcurementDbContext>((container, options) =>
            {
                //var connection = container.GetRequiredService<RetailProcurementDbContext>();
                options.UseInMemoryDatabase("integrationTestDb");
            });
        });
        builder.UseEnvironment("Development");
    }
}
