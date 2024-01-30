using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetailProcurement.WebAPI.Dtos;
using RetailProcurement.WebAPI.Entities;
using RetailProcurement.WebAPI.Persistence;
using RetailProcurement.WebAPI.Services;
using RetailProcurement.WebAPI.Services.Abstraction;

namespace RetailProcurement.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/statistics")]
public class StatisticsController : ControllerBase
{
    private readonly ILogger<StatisticsController> _logger;
    private readonly IStatisticsProvider _statisticsProvider;

    public StatisticsController(ILogger<StatisticsController> logger, RetailProcurementDbContext dbContext)
    {
        _logger = logger;
        _statisticsProvider = DataManagerFactory.GetStatisticsProvider(dbContext);
    }

    [HttpGet("best-offer/{storeItemId}")]
    public SupplierStoreItemDto? GetBestOffer(int storeItemId)
    {
        return _statisticsProvider.GetBestOffer(storeItemId);
    }

    [HttpGet("statistics/{id}")]
    public SupplierStatsDto GetSupplierStats(int id)
    {
        return _statisticsProvider.GetSupplierStatistics(id);
    }

    [HttpPost("quarterly-plan")]
    public void Post([FromBody] QuarterlyPlanDto quarterlyPlan)
    {
        _statisticsProvider.AddSupplierPlan(quarterlyPlan);
    }

    [HttpGet("quarterly-plan")]
    public List<SupplierDto> GetQuarterlyPlan()
    {
        var currentQuarter = ((DateTime.Now.Month - DateTime.Now.Month % 4) / 4) + 1;
        return _statisticsProvider.GetPlannedSuppliers(currentQuarter, DateTime.Now.Year);
    }
}
