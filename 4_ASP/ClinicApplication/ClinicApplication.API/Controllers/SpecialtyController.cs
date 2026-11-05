using ClinicApplication.Application.Services.Base;
using ClinicApplication.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialtyController : ControllerBase
    {

        private readonly ISpecialtyService _ISpecialtyService;
       // private readonly ISpecialtyService _SpecialtyService;

        public SpecialtyController(ISpecialtyService SpecialtyService)
        {
            _ISpecialtyService = SpecialtyService;
          //  _SpecialtyService = SpecialtyService;
        }


        [HttpGet("GetSpecialty")]
        public async Task<IActionResult> GetSpecialty()
        {
            var Specialty = await _ISpecialtyService.GetAllSpecialty();

            return Ok(new { message = "Get all Specialty", Specialty });
        }
    }



}
