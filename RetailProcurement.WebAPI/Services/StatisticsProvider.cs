using RetailProcurement.WebAPI.Dtos;
using RetailProcurement.WebAPI.Entities;
using RetailProcurement.WebAPI.Persistence;
using RetailProcurement.WebAPI.Services.Abstraction;

namespace RetailProcurement.WebAPI.Services
{
    public class StatisticsProvider : IStatisticsProvider
    {
        private readonly RetailProcurementDbContext _dbContext;
        public StatisticsProvider(RetailProcurementDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public SupplierStoreItemDto? GetBestOffer(int storeItemId)
        {
            var lowestSupplierStoreItem = _dbContext.SupplierStoreItems
                .Where(s => s.StoreItemId == storeItemId)
                .OrderBy(s => s.Price)
                .Take(1).FirstOrDefault();
            if (lowestSupplierStoreItem == null)
            {
                return null;
            }
            return new SupplierStoreItemDto() { 
                Id = lowestSupplierStoreItem!.Id, 
                Price = lowestSupplierStoreItem.Price!, 
                StoreItemId = lowestSupplierStoreItem.StoreItemId, 
                SupplierId = lowestSupplierStoreItem.SupplierId};
        }
        public SupplierStatsDto GetSupplierStatistics(int supplierId)
        {
            var supplierStats = _dbContext.Suppliers.Where(o => o.Id == supplierId).Select(o => new SupplierStatsDto
            {
                    Id = o.Id, 
                    TotalOrders = o.Orders == null ? 0 : o.Orders.Count(), 
                }).FirstOrDefault();
            return supplierStats!;
        }
        public List<SupplierDto> GetPlannedSuppliers(int quarter, int year)
        {
            var suppliers = _dbContext.QuarterlyPlans.Where(q => q.Quarter == quarter && q.Year == year)
                .Select(q => new SupplierDto { Id = q.SupplierId }).ToList();
            return suppliers;
        }
        public void AddSupplierPlan(QuarterlyPlanDto quarterlyPlan)
        {
            _dbContext.QuarterlyPlans.Add(new QuarterlyPlan() { SupplierId = quarterlyPlan.SupplierId, Quarter = quarterlyPlan.Quarter, Year = quarterlyPlan.Year });
            _dbContext.SaveChanges();
        }
    }
}
