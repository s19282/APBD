using AdvertApi.DTOs.Requests;
using AdvertApi.DTOs.Responses;
using AdvertApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace AdvertApi.Services
{
    public interface IAdvertDbService
    {
        public AddClientResponse AddClient(AddClientRequest req);
        public LoginClientResponse LoginClient(LoginClientRequest req);
        public RenewBearerTokenResponse RenewBearerToken(RenewBearerTokenRequest req);
        public ICollection<GetCampaignsResponse> GetCampaigns();
        public NewCampaignResponse NewCampaign(NewCampaignRequest req);
    }
}
