using ClinicApplication.Application.Services;
using ClinicApplication.Application.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {

        private readonly IPatientService _PatientService;

        public PatientsController(IPatientService PatientService)
        {
            _PatientService = PatientService;
        }


        [HttpGet("GetPatients")]
        public async Task<IActionResult> GetPatients()
        {
            var Patients = await _PatientService.GetAllPatient();

            return Ok(new { message = "Get all Users", Patients });
        }


    }
}
