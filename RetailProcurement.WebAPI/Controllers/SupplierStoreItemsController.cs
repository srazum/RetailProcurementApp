using Microsoft.AspNetCore.Mvc;
using RetailProcurement.WebAPI.Entities;
using RetailProcurement.WebAPI.Persistence;
using RetailProcurement.WebAPI.Services.Abstraction;
using RetailProcurement.WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using RetailProcurement.WebAPI.Dtos;

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
        public IEnumerable<SupplierStoreItemDto> Get()
        {
            return _supplierStoreItemOperations.GetAll().Select(e=> new SupplierStoreItemDto { Id = e.Id, Price = e.Price, StoreItemId = e.StoreItemId, SupplierId = e.SupplierId });
        }

        [HttpGet]
        [Route("{id}")]
        public SupplierStoreItemDto GetById(string id)
        {
            var supplierStoreItem = _supplierStoreItemOperations.GetById(id);
            return new SupplierStoreItemDto { Id = supplierStoreItem.Id, Price = supplierStoreItem.Price, StoreItemId = supplierStoreItem.StoreItemId, SupplierId = supplierStoreItem.SupplierId };
        }

        [HttpPost]
        public SupplierStoreItemDto Post([FromBody] SupplierStoreItemDto supplier)
        {
            var supplierStoreItem = new SupplierStoreItem { Id = supplier.Id, StoreItemId = supplier.StoreItemId, SupplierId = supplier.SupplierId, Price = supplier.Price };
            _supplierStoreItemOperations.Insert(supplierStoreItem);
            _supplierStoreItemOperations.Save();
            return new SupplierStoreItemDto { Id = supplierStoreItem.Id, Price = supplierStoreItem.Price, StoreItemId = supplierStoreItem.StoreItemId, SupplierId = supplierStoreItem.SupplierId };
        }

        [HttpPut("{id}")]
        public void Put([FromBody] SupplierStoreItemDto supplier, int id)
        {
            _supplierStoreItemOperations.Update(new SupplierStoreItem { Id = supplier.Id, StoreItemId = supplier.StoreItemId, SupplierId = supplier.SupplierId, Price = supplier.Price });
            _supplierStoreItemOperations.Save();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _supplierStoreItemOperations.Delete(id);
            _supplierStoreItemOperations.Save();
        }
    }
}
