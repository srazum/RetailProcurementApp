using Microsoft.EntityFrameworkCore;
using RetailProcurement.WebAPI.Dtos;
using RetailProcurement.WebAPI.Entities;
using RetailProcurement.WebAPI.Persistence;
using RetailProcurement.WebAPI.Services.Abstraction;

namespace RetailProcurement.WebAPI.Services
{
    public class OrderManager : IOrderManager
    { 

        private readonly RetailProcurementDbContext _dbContext;
        private readonly IGenericEntityOperations<Order> _orderOperations;
        private readonly IGenericEntityOperations<StoreItem> _storeItemOperations;

        public OrderManager(RetailProcurementDbContext dbContext) {
            _dbContext = dbContext;
            _orderOperations = new GenericEntityOperations<Order>(dbContext);
            _storeItemOperations = new GenericEntityOperations<StoreItem>(dbContext);
        }
        public IEnumerable<OrderDto> GetOrders()
        {
            return _orderOperations.GetAll().Select(e=> new OrderDto { Id = e.Id, SupplierId = e.SupplierId });
        }
        public OrderDto GetOrder(int orderId)
        {
            var order = _orderOperations.GetById(orderId);
            var orderDto = new OrderDto() {
                Id = order.Id,
                SupplierId = order.SupplierId,
            };
            var orderItems = _dbContext.OrderItemSuppliers.Where(e => e.OrderId == orderId);
            foreach(var orderItem in orderItems)
            {
                orderDto.Items = _dbContext.SupplierStoreItems.Where( e => e.Id == orderItem.SupplierStoreItemId)
                    .Include(e=> e.StoreItem).Select(e=> new StoreItemDto() { Id = e.StoreItem!.Id, Name = e.StoreItem!.Name }).ToList();
            }
            return orderDto;
        }
        public bool CreateOrder(OrderDto orderDto)
        {
            var order = _dbContext.Orders.Add(new Order() { SupplierId = orderDto.SupplierId });
            _dbContext.SaveChanges();
            foreach (var item in orderDto.Items!)
            {
                var storeItem = _storeItemOperations.GetById(item.Id);
                var supplierStoreItem = _dbContext.SupplierStoreItems.FirstOrDefault(e => e.StoreItemId == item.Id && e.SupplierId == orderDto.SupplierId);
                _dbContext.OrderItemSuppliers.Add(new OrderItemSupplier() { OrderId = order.Entity.Id, SupplierStoreItemId = supplierStoreItem!.Id, Price = supplierStoreItem.Price });
            }
            _dbContext.SaveChanges();
            return true;
        }
    }
}
