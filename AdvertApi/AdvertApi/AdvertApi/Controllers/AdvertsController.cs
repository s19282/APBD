using Microsoft.AspNetCore.Mvc;

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