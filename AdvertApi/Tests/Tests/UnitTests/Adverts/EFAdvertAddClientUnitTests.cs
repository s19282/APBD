using AdvertApi.DTOs.Requests;
using AdvertApi.Model;
using AdvertApi.Models;
using AdvertApi.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.UnitTests.Adverts
{
    [TestFixture]
    class EFAdvertAddClientUnitTests
    {
        [Test]
        public void AddClientMethod_CompleteRequest_Correct()
        {
            var area = new Mock<IAdvertService>();
            List<Building> buildings = new List<Building> { new Building { IdBuilding=1,Street="s",StreetNumber=1,City="c",Height=5},
                                                            new Building { IdBuilding=2,Street="s",StreetNumber=2,City="c",Height=10}};
            Campaign campaign = new Campaign { IdCampaign = 1, IdClient = 1, StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(10), PricePerSquareMeter = 10, FromIdBuilding = 1, ToIdBuilding = 2 };
            area.Setup(e => e.calculateArea(buildings, campaign, 10)).Returns(new List<Banner> {
                new Banner { IdAdvertisment=1,Name=1,Price=50,IdCampaign=1,Area=5},
                new Banner { IdAdvertisment = 2, Name = 2, Price = 100, IdCampaign = 1, Area = 10 }});
            var cont = new EfAdvertDbService(new AdvertsDbContext(), new ConfigurationBuilder().Build(),area.Object);

            var result = cont.AddClient(new AddClientRequest { FirstName = "Fname", LastName = "Lname", Email = "Email", Phone = "Phone", Login = "Login", Password = "zaq1@WSX" });

            Assert.IsTrue(result.FirstName.Equals("Fname"));
            Assert.IsTrue(result.LastName.Equals("Lname"));
            Assert.IsTrue(result.Email.Equals("Email"));
            Assert.IsTrue(result.Phone.Equals("Phone"));
            Assert.IsTrue(result.Login.Equals("Login"));
        }
    }
}
