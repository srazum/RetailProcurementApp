using RetailProcurement.WebAPI.Dtos;
using RetailProcurement.WebAPI.Entities;

namespace RetailProcurement.WebAPI.Services.Abstraction
{
    public interface IOrderManager
    {
        IEnumerable<OrderDto> GetOrders();
        OrderDto GetOrder(int orderId);
        bool CreateOrder(OrderDto order);
    }
}
