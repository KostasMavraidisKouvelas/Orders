using Microsoft.AspNetCore.Mvc;
using Orders.Application;

namespace Orders.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProducstsController : Controller
    {
        private readonly IOperations _operations;
        public ProducstsController(IOperations operations)
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
