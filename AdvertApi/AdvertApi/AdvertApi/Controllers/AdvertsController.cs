using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;

namespace AdvertApi.Model
{
    [Route("api/adverts")]
    [ApiController]
    public class AdvertsController : ControllerBase
    {
        private readonly AdvertsDbContext _context;
        public AdvertsController(AdvertsDbContext context)
        {
            _context = context;
        }

        public IActionResult GetAdverts()
        {
            return Ok();
        }
    }
}