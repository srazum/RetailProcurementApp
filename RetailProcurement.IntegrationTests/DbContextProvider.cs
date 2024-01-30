using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailProcurement.IntegrationTests
{
    internal class DbContextProvider
    {
        public static RetailProcurementDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<RetailProcurementDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase") // Create a new in-memory database for each test
                .Options;
            return new RetailProcurementDbContext(options);
        }
    }
}
