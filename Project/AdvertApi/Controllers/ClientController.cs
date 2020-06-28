using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AdvertApi.DTOs.Requests;
using AdvertApi.Service;

namespace AdvertApi.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ICampaignService _dbService;

        public ClientController(ICampaignService dbService)
        {
            this._dbService = dbService;
        }

        [HttpPost]
        public async Task<IActionResult> RegistrateClient(RegistrateNewClientRequest newClientRequest)
        {
            var result = await _dbService.RegistrateClientAsync(newClientRequest);
            ObjectResult response = new ObjectResult(result)
            {
                StatusCode = 201
            };
            return response;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var result = await _dbService.LoginAsync(loginRequest);
            ObjectResult response = new ObjectResult(result)
            {
                StatusCode = 201
            };
            return response;
        }

        [HttpPost("refresh-token/{refreshToken}")]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            var result = await _dbService.CreateNewRefreshAndAccessTokenAsync(refreshToken);
            ObjectResult response = new ObjectResult(result)
            {
                StatusCode = 201
            };
            return response;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCampaings()
        {
            var result = await _dbService.GetCampaignsAsync();
            ObjectResult response = new ObjectResult(result)
            {
                StatusCode = 200
            };
            return response;
        }

        [HttpPost("campaign")]
        [Authorize]
        public async Task<IActionResult> CreateCampaign(CreateNewCampaignRequest newCampaignRequest)
        {
            var result = await _dbService.CreateCampaignAsync(newCampaignRequest);
            ObjectResult response = new ObjectResult(result)
            {
                StatusCode = 201
            };
            return response;
        }
    }
}
