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

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _operations.GetProductsAsync();
            return Ok(products);
        }

        [HttpPost]
        [Route("Import")]
        public async Task<IActionResult> ImportProducts()
        {
            await _operations.ImportProductsAsync();
            return Ok();
        }
    }
}
