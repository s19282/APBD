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
    class AdvertsNewCampaignIntegrationTests
    {
        [Test]
        public void NewCampaignMehtod_RequestCorrect_Correct()
        {
            var controller = new AdvertsController(new EfAdvertDbService(new AdvertsDbContext(), new ConfigurationBuilder().Build(), new CalculateAreaService()));

            var result = controller.NewCampaign(new AdvertApi.DTOs.Requests.NewCampaignRequest { IdClient = 1, FromIdBuilding = 3, ToIdBuilding = 6 });

            Assert.IsNotNull(result);
            Assert.IsTrue(result is CreatedResult);
        }
    }
}
