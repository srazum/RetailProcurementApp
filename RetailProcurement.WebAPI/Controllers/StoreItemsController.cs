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
    public IEnumerable<StoreItemDto> Get()
    {
        return _storeItemsOperations.GetAll().Select(e => new StoreItemDto { Id = e.Id, Name = e.Name  });
    }

    [HttpGet("{id}")]
    public StoreItemDto GetById(string id)
    {
        var storeItem = _storeItemsOperations.GetById(id);
        return new StoreItemDto { Id = storeItem.Id, Name = storeItem.Name };
    }
    [HttpPost]
    public void Post([FromBody] StoreItemDto storeItem)
    {
        _storeItemsOperations.Insert(new StoreItem { Id = storeItem.Id, Name = storeItem.Name });
        _storeItemsOperations.Save();
    }
    [HttpPut("{id}")]
    public void Put([FromBody] StoreItemDto storeItem, string id)
    {
        _storeItemsOperations.Update(new StoreItem { Id = storeItem.Id, Name = storeItem.Name });
        _storeItemsOperations.Save();
    }
    [HttpDelete("{id}")]
    public void Delete(string id)
    {
        _storeItemsOperations.Delete(id);
        _storeItemsOperations.Save();
    }
}
