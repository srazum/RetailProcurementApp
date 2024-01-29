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
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> _logger;
    private readonly IOrderManager _orderManager;

    public OrdersController(ILogger<OrdersController> logger, RetailProcurementDbContext dbContext)
    {
        _logger = logger;
        _orderManager = DataManagerFactory.GetOrderManager(dbContext);
    }

    [HttpGet]
    public IEnumerable<OrderDto> Get()
    {
        return _orderManager.GetOrders();
    }

    [HttpGet("{id}")]
    public OrderDto GetById(int id)
    {
        return _orderManager.GetOrder(id);
    }
    [HttpPost]
    public void Post([FromBody] OrderDto order)
    {
        _orderManager.CreateOrder(order);
    }
}
