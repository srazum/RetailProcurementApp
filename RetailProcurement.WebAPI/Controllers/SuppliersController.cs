using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public IEnumerable<Supplier> Get()
    {
        return _supplierOperations.GetAll();
    }

    [HttpGet]
    [Route("{id}")]
    public Supplier GetById(string id)
    {
        return _supplierOperations.GetById(id);
    }

    [HttpPost]
    public void Post([FromBody] Supplier supplier)
    {
        _supplierOperations.Insert(supplier);
        _supplierOperations.Save();
    }

    [HttpPut("{id}")]
    public void Put([FromBody] Supplier supplier, string id)
    {
        _supplierOperations.Update(supplier);
        _supplierOperations.Save();
    }

    [HttpDelete("{id}")]
    public void Delete(string id)
    {
        _supplierOperations.Delete(id);
        _supplierOperations.Save();
    }
}
