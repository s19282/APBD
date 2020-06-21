using AdvertApi.Model;
using AdvertApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.IntegrationTests.Adverts
{
    [TestFixture]
    class AdvertsRenewBearerTokenIntegrationTests
    {
        [Test]
        public void RenewBearerTokenMethod_RequestComplete_Correct()
        {
            var controller = new AdvertsController(new EfAdvertDbService(new AdvertsDbContext(), new ConfigurationBuilder().Build(), new CalculateAreaService()));


            var result = controller.RenewBearerToken(new AdvertApi.DTOs.Requests.RenewBearerTokenRequest { RefreshToken = "R" });

            Assert.IsNotNull(result);
            Assert.IsTrue(result is OkObjectResult);
        }
    }
}
