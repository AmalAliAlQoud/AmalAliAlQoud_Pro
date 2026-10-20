using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClinicApplication.Application.Services.Base;

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


        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var Users = await _UserService.GetAllUser();

            return Ok(new { message = "Get all Users", Users });
        }
    }
}
