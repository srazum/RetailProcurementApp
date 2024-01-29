using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetailProcurement.WebAPI.Entities;
using RetailProcurement.WebAPI.Persistence;
using RetailProcurement.WebAPI.Services;
using RetailProcurement.WebAPI.Services.Abstraction;

namespace RetailProcurement.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/store-items")]
public class StoreItemsController : ControllerBase
{
    private readonly ILogger<StoreItemsController> _logger;
    private readonly IGenericEntityOperations<StoreItem> _storeItemsOperations;

    public StoreItemsController(ILogger<StoreItemsController> logger, RetailProcurementDbContext dbContext)
    {
        _logger = logger;
        _storeItemsOperations = new GenericEntityOperations<StoreItem>(dbContext);
    }

    [HttpGet]
    public IEnumerable<StoreItem> Get()
    {
        return _storeItemsOperations.GetAll();
    }

    [HttpGet("{id}")]
    public StoreItem GetById(string id)
    {
        return _storeItemsOperations.GetById(id);
    }
    [HttpPost]
    public void Post([FromBody] StoreItem storeItem)
    {
        _storeItemsOperations.Insert(storeItem);
        _storeItemsOperations.Save();
    }
    [HttpPut("{id}")]
    public void Put([FromBody] StoreItem storeItem, string id)
    {
        _storeItemsOperations.Update(storeItem);
        _storeItemsOperations.Save();
    }
    [HttpDelete("{id}")]
    public void Delete(string id)
    {
        _storeItemsOperations.Delete(id);
        _storeItemsOperations.Save();
    }
}
