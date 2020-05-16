using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cw11.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;

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