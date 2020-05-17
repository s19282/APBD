using Cw11.Models;
using Cw11.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cw11.Controllers
{
    [Route("api/hospital")]
    [ApiController]
    public class HospitalController : ControllerBase
    {

        private readonly IHospitalDbService _context;
        public HospitalController(IHospitalDbService context)
        {
            _context = context;
        }
        [HttpGet("doctors")]
        public IActionResult GetDoctor()
        {
            return Ok(_context.GetDoctors());
        }

    }
}