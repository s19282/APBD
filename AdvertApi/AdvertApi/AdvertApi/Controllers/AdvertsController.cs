using AdvertApi.DTOs.Requests;
using AdvertApi.DTOs.Responses;
using AdvertApi.Exceptions;
using AdvertApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvertApi.Model
{
    [Route("api/adverts")]
    [ApiController]
    public class AdvertsController : ControllerBase
    {
        private IAdvertDbService _dbService;
        public AdvertsController(IAdvertDbService dbService)
        {
            _dbService = dbService;
        }


        [HttpPost("clients")]
        public IActionResult AddClient(AddClientRequest req)
        {
            try
            {
                AddClientResponse resp = _dbService.AddClient(req);
                return Created("", resp);
            }
            catch(LoginAlreadyTakenException exc)
            {
                return BadRequest(exc);
            }
        }

        [HttpPost("clients/login")]
        public IActionResult LoginClient(LoginClientRequest req)
        {
            try
            {
                return Ok(_dbService.LoginClient(req));
            }
            catch(IncorrectLoginException exc)
            {
                return BadRequest(exc);
            }
            catch(IncorrectPasswordException exc)
            {
                return BadRequest(exc);
            }
        }
        [HttpPost("clients/refresh")]
        public IActionResult RenewBearerToken(RenewBearerTokenRequest req)
        {
            try
            {
                return Ok(_dbService.RenewBearerToken(req));
            }
            catch(RefreshTokenIncorrectException exc)
            {
                return NotFound(exc);
            }
        }
        [HttpPost("campaigns")]
        [Authorize]
        public IActionResult GetCampaings()
        {
            return Ok(_dbService.GetCampaigns());
        }
        [HttpPost("campaigns/create")]
        [Authorize]
        public IActionResult NewCampaign(NewCampaignRequest req)
        {
           try
            {
                return Created("", _dbService.NewCampaign(req));
            }
            catch(NoBuildingsException exc)
            {
                return NotFound(exc);
            }
            catch(BuildingAreNotNextToEachOtherException exc)
            {
                return BadRequest(exc);
            }
        }
    }
}
