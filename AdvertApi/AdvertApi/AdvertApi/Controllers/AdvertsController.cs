﻿using AdvertApi.DTOs.Requests;
using AdvertApi.DTOs.Responses;
using AdvertApi.Models;
using Microsoft.AspNetCore.Authorization;
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
                expires: DateTime.Now.AddMinutes(60),
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
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds
            );
            _context.SaveChanges();
            return Ok(new RenewBearerTokenResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken.ToString()
            });
        }
        [HttpPost("campaigns")]
        [Authorize]
        public IActionResult GetCampaings()
        {
            var campaigns = _context.Campaigns
                .Join(_context.Clients, ca => ca.IdClient, cl => cl.IdClient, (ca, cl) =>
                      new { Campaign = ca, Client = cl })
                .OrderByDescending(c => c.Campaign.StartDate);
            return Ok(campaigns);
        }
        [HttpPost("campaigns/create")]
        [Authorize]
        public IActionResult NewCampaign(NewCampaignRequest req)
        {
            if (_context.Buildings.Count() < 2)
                return NotFound("No buildings in the database");
            var B1 = _context.Buildings.Where(b => b.IdBuilding.Equals(req.FromIdBuilding)).FirstOrDefault();
            var B2 = _context.Buildings.Where(b => b.IdBuilding.Equals(req.ToIdBuilding)).FirstOrDefault();

            if (!B1.Street.Equals(B2.Street))
                return BadRequest("The buildings are not next to each other");

            var buildings = _context.Buildings.Where(b => b.StreetNumber >= B1.StreetNumber && b.StreetNumber <= B2.StreetNumber).OrderBy(b=>b.StreetNumber).ToList();
            int howManyBuildings = buildings.Count();

            Campaign campaign = new Campaign { IdClient = req.IdClient, StartDate = req.StartDate, EndDate = req.EndDate, PricePerSquareMeter = req.PricePerSquareMeter, FromIdBuilding = req.FromIdBuilding, ToIdBuilding = req.ToIdBuilding };
            _context.Campaigns.Add(campaign);
            _context.SaveChanges();

            Banner banner1 = new Banner { };
            Banner banner2 = new Banner { };

            for (int i=1; i<howManyBuildings-1; i++)
            {
                decimal height1Max = 0;
                decimal height2Max = 0;
                Banner tmpBanner1 = new Banner { Name = 1,IdCampaign = campaign.IdCampaign };
                Banner tmpBanner2 = new Banner { Name = 2,IdCampaign = campaign.IdCampaign };
                for(int j=0; j<howManyBuildings; j++)
                {
                    var tmpBuilding = buildings.ElementAt(j);

                    if (j < i && tmpBuilding.Height > height1Max)
                        height1Max = tmpBuilding.Height;

                    if (j == i)
                    {
                        tmpBanner1.Area = height1Max * i;
                        tmpBanner1.Price = tmpBanner1.Area * req.PricePerSquareMeter;
                    }

                    if (j >= i && tmpBuilding.Height > height2Max)
                        height2Max = tmpBuilding.Height;
                }
                tmpBanner2.Area = height2Max * (howManyBuildings-i);
                tmpBanner2.Price = tmpBanner2.Area * req.PricePerSquareMeter;
                if ((banner1.Area == 0 && banner2.Area == 0) || (tmpBanner1.Area + tmpBanner2.Area < banner1.Area + banner2.Area))
                {
                    banner1 = tmpBanner1;
                    banner2 = tmpBanner2;
                }
            }

            _context.Banners.AddRange(banner1, banner2);
            _context.SaveChanges();

            return Created("",new NewCampaignResponse{ Campaign=campaign,Banner1=banner1,Banner2=banner2 });
        }
    }
}
