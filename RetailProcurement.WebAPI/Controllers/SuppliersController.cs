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
[Route("api/suppliers")]
public class SupplierController : ControllerBase
{
    private readonly ILogger<SupplierController> _logger;
    private readonly IGenericEntityOperations<Supplier> _supplierOperations;

    public SupplierController(ILogger<SupplierController> logger, RetailProcurementDbContext dbContext)
    {
        _logger = logger;
        _supplierOperations = new GenericEntityOperations<Supplier>(dbContext);
    }

    [HttpGet]
    public IEnumerable<SupplierDto> Get()
    {
        return _supplierOperations.GetAll().Select(e => new SupplierDto {  Id = e.Id, Name = e.Name });
    }

    [HttpGet]
    [Route("{id}")]
    public SupplierDto GetById(string id)
    {
        var supplier = _supplierOperations.GetById(id);
        return new SupplierDto { Id = supplier.Id, Name = supplier.Name };
    }

    [HttpPost]
    public void Post([FromBody] SupplierDto supplier)
    {
        _supplierOperations.Insert(new Supplier { Id = supplier.Id, Name = supplier.Name });
        _supplierOperations.Save();
    }

    [HttpPut("{id}")]
    public void Put([FromBody] SupplierDto supplier, string id)
    {
        _supplierOperations.Update(new Supplier { Id = supplier.Id, Name = supplier.Name });
        _supplierOperations.Save();
    }

    [HttpDelete("{id}")]
    public void Delete(string id)
    {
        _supplierOperations.Delete(id);
        _supplierOperations.Save();
    }
}
