using RetailProcurement.WebAPI.Dtos;
using RetailProcurement.WebAPI.Entities;

namespace RetailProcurement.WebAPI.Services.Abstraction
{
    public interface IStatisticsProvider
    {
        SupplierStoreItemDto? GetBestOffer(int storeItemId);
        SupplierStatsDto GetSupplierStatistics(int supplierId);
        List<SupplierDto> GetPlannedSuppliers(int quarter, int year);
        void AddSupplierPlan(QuarterlyPlanDto quarterlyPlan);
    }
}
