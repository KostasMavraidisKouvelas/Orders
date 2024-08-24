using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Orders.Application;
using Orders.DTO;

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
            await _operations.CreateOrderAsync(order);
            return Ok();
        }
        
    }
}
