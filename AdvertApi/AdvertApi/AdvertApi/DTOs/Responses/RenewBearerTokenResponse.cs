using Newtonsoft.Json;

namespace AdvertApi.DTOs.Responses
{
    public class RenewBearerTokenResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
