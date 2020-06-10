using AdvertApi.DTOs.Requests;
using AdvertApi.DTOs.Responses;
using AdvertApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;


namespace AdvertApi.Model
{
    [Route("api/adverts")]
    [ApiController]
    public class AdvertsController : ControllerBase
    {
        private readonly AdvertsDbContext _context;
        public IConfiguration Configuration { get; set; }
        public AdvertsController(AdvertsDbContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetAdverts()
        {
            return Ok();
        }

        [HttpPost("clients")]
        public IActionResult AddClient(AddClientRequest req)
        {
            if (_context.Clients.Where(c => c.Login.Equals(req.Login)).FirstOrDefault() != null)
                return BadRequest("This login is already taken");
            Client client = new Client { FirstName = req.FirstName, LastName =req.LastName,Email=req.Email,Phone=req.Phone,Login=req.Login,Password= BCrypt.Net.BCrypt.HashPassword(req.Password,BCrypt.Net.SaltRevision.Revision2)};
            _context.Clients.Add(client);
            _context.SaveChanges();
            AddClientResponse resp = new AddClientResponse { FirstName = client.FirstName, LastName = client.LastName, Email = client.Email, Phone = client.Phone, Login = client.Login };
            return Created("",resp);
        }
        [HttpPost("clients/login")]
        public IActionResult LoginClient(LoginClientRequest req)
        {
            var userExists = _context.Clients.Where(c => c.Login.Equals(req.Login)).FirstOrDefault();
            if (userExists == null)
                return Unauthorized("Incorrect login");
            if (!BCrypt.Net.BCrypt.Verify(req.Password, userExists.Password))
                return Unauthorized("Incorrect password");

            Guid refreshToken = Guid.NewGuid();
            userExists.RefreshToken = refreshToken.ToString();
            _context.Update(userExists);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userExists.Login),
                new Claim(ClaimTypes.Name, userExists.FirstName+" "+userExists.LastName),
                new Claim(ClaimTypes.Role, "client")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: "s19282",
                audience: "Clients",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
            );
            _context.SaveChanges();
            return Ok(new LoginClientResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken.ToString()
            });
        }
        [HttpPost("clients/refresh")]
        public IActionResult RenewBearerToken(RenewBearerTokenRequest req)
        {
            var client = _context.Clients.Where(c => c.RefreshToken.Equals(req.RefreshToken)).FirstOrDefault();
            if (client == null)
                return NotFound("Refresh token incorrect");

            Guid refreshToken = Guid.NewGuid();
            client.RefreshToken = refreshToken.ToString();
            _context.Update(client);

            var claims = new[]
             {
                new Claim(ClaimTypes.NameIdentifier, client.Login),
                new Claim(ClaimTypes.Name, client.FirstName+" "+client.LastName),
                new Claim(ClaimTypes.Role, "client")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: "s19282",
                audience: "Clients",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
            );
            _context.SaveChanges();
            return Ok(new RenewBearerTokenResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken.ToString()
            });
        }

    }
}
