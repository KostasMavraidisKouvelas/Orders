using Microsoft.AspNetCore.Mvc;

namespace Orders.Api.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
