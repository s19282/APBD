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

namespace Tests.UnitTests.Adverts
{
    [TestFixture]
    class AdvertsGetCampaignsUnitTests
    {
        [Test]
        public void GetCampaignsMehtod_CampaingsExists_Correct()
        {
            var dbLayer = new Mock<IAdvertDbService>();
            dbLayer.Setup(d => d.GetCampaigns()).Returns(new List<GetCampaignsResponse>()
            {
                new GetCampaignsResponse{Client = new Client{FirstName="Fname"},Campaign = new Campaign{PricePerSquareMeter=54}}
            });
            var cont = new AdvertsController(dbLayer.Object);

            var result = cont.GetCampaings();

            Assert.IsNotNull(result);
            Assert.IsTrue(result is OkObjectResult);
        }
    }
}
