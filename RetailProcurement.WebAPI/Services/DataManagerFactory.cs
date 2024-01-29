using RetailProcurement.WebAPI.Persistence;
using RetailProcurement.WebAPI.Services.Abstraction;

namespace RetailProcurement.WebAPI.Services
{
    public static class DataManagerFactory
    {
        public static IOrderManager GetOrderManager(RetailProcurementDbContext dbContext)
        {
            return new OrderManager(dbContext);
        }
        public static IStatisticsProvider GetStatisticsProvider(RetailProcurementDbContext dbContext)
        {
            return new StatisticsProvider(dbContext);
        }
    }
}
