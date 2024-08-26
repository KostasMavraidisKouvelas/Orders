using Microsoft.AspNetCore.Mvc;
using Orders.Application;
using Orders.DTO;

namespace Orders.Api.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("api/[controller]/Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerDto)
        {
            try
            {
                await _userService.Register(registerDto);
                return Ok();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
               
            }
        }

        [HttpPost]
        [Route("api/[controller]/Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var token = await _userService.Login(loginDto);
            return Ok(token);
        }
    }



}
