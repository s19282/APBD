using AdvertApi.DTOs.Responses;
using AdvertApi.Model;
using AdvertApi.Models;
using AdvertApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.IntegrationTests.Adverts
{
    [TestFixture]
    class AdvertsGetCampaignsIntegrationTests
    {
        [Test]
        public void GetCampaignsMehtod_CampaingsExists_Correct()
        {
            var controller = new AdvertsController(new EfAdvertDbService(new AdvertsDbContext(), new ConfigurationBuilder().Build(), new CalculateAreaService()));

            var result = controller.GetCampaings();

            Assert.IsNotNull(result);
            Assert.IsTrue(result is OkObjectResult);
        }
    }
}
