using System.Collections.Generic;
using System.Threading.Tasks;
using AdvertApi.DTOs.Requests;
using AdvertApi.DTOs.Responses;
using Project_AdvertApi.DTOs.Responses;

namespace AdvertApi.Service
{
    public interface ICampaignService
    {
        public Task<RegistratedClientResponse> RegistrateClientAsync(RegistrateNewClientRequest newClientRequest);
        public Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
        public Task<LoginResponse> CreateNewRefreshAndAccessTokenAsync(string refreshToken);
        public Task<IEnumerable<CampaignResponse>> GetCampaignsAsync();
        public Task<NewlyCreatedCampaignResponse> CreateCampaignAsync(CreateNewCampaignRequest request);
    }
}
