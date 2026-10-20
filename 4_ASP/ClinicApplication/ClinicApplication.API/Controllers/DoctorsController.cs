using ClinicApplication.Application.Services;
using ClinicApplication.Application.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {

        private readonly IDoctorService _DoctorService;
        private readonly ISpecialtyService _SpecialtyService;

        public DoctorsController(IDoctorService DoctorService, ISpecialtyService SpecialtyService)
        {
            _DoctorService = DoctorService;
            _SpecialtyService = SpecialtyService;
        }


        [HttpGet("GetDoctors")]
        public async Task<IActionResult> GetDoctors()
        {
            var Doctors = await _DoctorService.GetAllDoctor();

            return Ok(new { message = "Get all Doctors", Doctors });
        }
    }
}
