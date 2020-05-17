using Cw11.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cw11.Controllers
{
    [Route("api/hospital")]
    [ApiController]
    public class HospitalController : ControllerBase
    {

        private readonly HospitalDbContext _context;
        public HospitalController(HospitalDbContext context)
        {
            _context = context;
        }
        public IActionResult GetDoctor()
        {
            return Ok();
        }
    }
}