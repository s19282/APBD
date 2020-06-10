using AdvertApi.DTOs.Requests;
using AdvertApi.DTOs.Responses;
using AdvertApi.Models;
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

        [HttpGet]
        public IActionResult GetAdverts()
        {
            return Ok();
        }

        [HttpPost("clients")]
        public IActionResult AddClient(AddClientRequest req)
        {
            int a = 5;
            Client client = new Client { FirstName = req.FirstName, LastName =req.LastName,Email=req.Email,Phone=req.Phone,Login=req.Login,Password= BCrypt.Net.BCrypt.HashPassword(req.Password,BCrypt.Net.SaltRevision.Revision2)};
            _context.Clients.Add(client);
            _context.SaveChanges();
            AddClientResponse resp = new AddClientResponse { FirstName = client.FirstName, LastName = client.LastName, Email = client.Email, Phone = client.Phone, Login = client.Login };
            return Created("",resp);
        }
        [HttpPost("clients/login")]
        public IActionResult LoginClient(LoginClientRequest req)
        {
            return Ok();
        }
    }
}