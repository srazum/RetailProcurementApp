using Microsoft.AspNetCore.Mvc;
using RetailProcurement.WebAPI.Entities;
using RetailProcurement.WebAPI.Persistence;
using RetailProcurement.WebAPI.Services.Abstraction;
using RetailProcurement.WebAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace RetailProcurement.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/supplier-store-items")]
    public class SupplierStoreItemsController
    {
        private readonly ILogger<SupplierStoreItemsController> _logger;
        private readonly IGenericEntityOperations<SupplierStoreItem> _supplierStoreItemOperations;

        public SupplierStoreItemsController(ILogger<SupplierStoreItemsController> logger, 
            RetailProcurementDbContext dbContext)
        {
            _logger = logger;
            _supplierStoreItemOperations = new GenericEntityOperations<SupplierStoreItem>(dbContext);
        }

        [HttpGet]
        public IEnumerable<SupplierStoreItem> Get()
        {
            return _supplierStoreItemOperations.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public SupplierStoreItem GetById(string id)
        {
            return _supplierStoreItemOperations.GetById(id);
        }

        [HttpPost]
        public void Post([FromBody] SupplierStoreItem supplier)
        {
            _supplierStoreItemOperations.Insert(supplier);
            _supplierStoreItemOperations.Save();
        }

        [HttpPut("{id}")]
        public void Put([FromBody] SupplierStoreItem supplier, string id)
        {
            _supplierStoreItemOperations.Update(supplier);
            _supplierStoreItemOperations.Save();
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _supplierStoreItemOperations.Delete(id);
            _supplierStoreItemOperations.Save();
        }
    }
}
