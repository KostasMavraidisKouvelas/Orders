using Microsoft.AspNetCore.Mvc;
using Orders.Application;

namespace Orders.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IOperations _operations;
        public ProductsController(IOperations operations)
        {
            operations = _operations;
        }
        [HttpPost]
        [Route("Import")]
        public async Task<IActionResult> ImportProducts()
        {
            await _operations.ImportProducts();
            return Ok();
        }
    }
}
