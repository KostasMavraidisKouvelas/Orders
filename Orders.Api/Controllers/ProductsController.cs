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
            _operations = operations;
        }

        [HttpGet]
        [Route("Id")]
        public async Task<IActionResult> GetProduct([FromRoute] int Id)
        {
            var products = await _operations.GetProduct(Id);
            return Ok(products);
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
