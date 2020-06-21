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
    class AdvertsLoginClientIntegrationTests
    {
        [Test]
        public void LoginClientMethod_ClientExists_Correct()
        {
            var controller = new AdvertsController(new EfAdvertDbService(new AdvertsDbContext(), new ConfigurationBuilder().Build(), new CalculateAreaService()));


            var result = controller.LoginClient(new AdvertApi.DTOs.Requests.LoginClientRequest { Login = "login", Password = "Password" });

            Assert.IsNotNull(result);
            Assert.IsTrue(result is OkObjectResult);
        }
    }
}
