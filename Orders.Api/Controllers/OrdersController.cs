using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orders.Application;
using Orders.DTO;
using System.Security.Claims;

namespace Orders.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly IOperations _operations;


        public OrdersController(IOperations operations)
        {
            _operations = operations;
        }
        [HttpPost]
        [Authorize(Policy = "CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDto order)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            await _operations.CreateOrderAsync(order,userEmail);
            return Ok();
        }

        [HttpGet]
        [Authorize(Policy = "ViewOrders")]
        public async Task<IActionResult> GetOrders()
        {

            var orders = await _operations.GetOrdersAsync();
            return Ok(orders);
        }

        [HttpPut]
        [Authorize(Policy = "EditOrders")]
        public async Task<IActionResult> SetOrderDispatched([FromRoute] int orderId)
        {
            var order = await _operations.SetOrderDispatchedAsync(orderId);
            return Ok(order);
        }

        [HttpGet]
        [Authorize(Policy = "ResendInvoice")]
        public async Task<IActionResult> ResendInvoice([FromRoute] int orderId)
        {
            await _operations.ResendInvoiceAsync(orderId);
            return Ok();
        }
    }
}
