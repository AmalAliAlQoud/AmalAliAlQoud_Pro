using ClinicApplication.Application.Dtos;
using ClinicApplication.Application.Services;
using ClinicApplication.Application.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _UserService;

        public UsersController(IUserService UserService)
        {
            _UserService = UserService;
        }


        //####################################################################
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            var user = _UserService.Login(loginDto);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            return Ok(new
            {

                message = "Login successful",
                id = user.Id,
                name = user.Name,
                username = user.UserName,
                email = user.Email,
            });
        }



        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterDto registerDto)
        {
            var user = _UserService.Register(registerDto);
            if (user == null)
            {
                return BadRequest(new { message = "Username or email already exists" });
            }
            return Ok(new { message = "Registration successful", user });
        }


        //####################################################################


        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var Users = await _UserService.GetAllUser();

            return Ok(new { message = "Get all Users", Users });
        }
    }
}
