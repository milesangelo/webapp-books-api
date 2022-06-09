using Books.Api.Models;
using Books.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Books.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService service)
        {
            _userService = service;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Post([FromBody] LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest();
            }

            var loginResponse = await _userService.LoginAsync(request);

            if (loginResponse == null) return BadRequest("Invalid credentials");

            return Ok(loginResponse);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok("great");
        }



    }
}
