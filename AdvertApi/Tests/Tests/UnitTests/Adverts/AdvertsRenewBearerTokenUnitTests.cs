using AdvertApi.DTOs.Responses;
using AdvertApi.Model;
using AdvertApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Tests.UnitTests.Adverts
{
    [TestFixture]
    class AdvertsRenewBearerTokenUnitTests
    {
        [Test]
        public void RenewBearerTokenMethod_RequestComplete_Correct()
        {
            var dbLayer = new Mock<IAdvertDbService>();
            dbLayer.Setup(e => e.RenewBearerToken(new AdvertApi.DTOs.Requests.RenewBearerTokenRequest { RefreshToken = "R" }))
                .Returns(new RenewBearerTokenResponse { RefreshToken = "rr", AccessToken = "aaac" });
            var cont = new AdvertsController(dbLayer.Object);

            var result = cont.RenewBearerToken(new AdvertApi.DTOs.Requests.RenewBearerTokenRequest { RefreshToken = "R" });

            Assert.IsNotNull(result);
            Assert.IsTrue(result is OkObjectResult);
        }       
    }
}
