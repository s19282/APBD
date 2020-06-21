using AdvertApi.DTOs.Requests;
using AdvertApi.Model;
using AdvertApi.Services;
using Castle.Core.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.IntegrationTests.Adverts
{
    [TestFixture]
    class AdvertsAddClientIntegrationTests
    {
        [Test]
        public void AddClientMethod_CompleteRequest_Correct()
        {
            var controller = new AdvertsController(new EfAdvertDbService(new AdvertsDbContext(),new ConfigurationBuilder().Build(),new CalculateAreaService()));
            AddClientRequest req = new AddClientRequest { FirstName = "Fname", LastName = "Lname", Email = "Email", Phone = "Phone", Login = "Login6", Password = "zaq1@WSX" };

            var result = controller.AddClient(req);

            Assert.IsNotNull(result);
            Assert.IsTrue(result is CreatedResult);
        }
        [Test]
        public void AddClientMethod_IncompleteRequest_Incorrect()
        {
            AddClientRequest req = new AddClientRequest { Email = "Email", Phone = "Phone", Login = "Login", Password = "zaq1@WSX" };
            var context = new ValidationContext(req, null, null);
            var results = new List<ValidationResult>();

            var isModelStateValid = Validator.TryValidateObject(req, context, results, true);

            Assert.IsFalse(isModelStateValid);
            Assert.IsTrue(results.Count == 2);
        }
        [Test]
        public void AddClientMethod_PasswordTooShort_Incorrect()
        {
            AddClientRequest req = new AddClientRequest { FirstName = "Fname", LastName = "Lname", Email = "Email", Phone = "Phone", Login = "Login", Password = "zaq" };
            var context = new ValidationContext(req, null, null);
            var results = new List<ValidationResult>();

            var isModelStateValid = Validator.TryValidateObject(req, context, results, true);

            Assert.IsFalse(isModelStateValid);
            Assert.IsTrue(results.Count == 1);
            Assert.IsTrue(results[0].MemberNames.ElementAt(0) == nameof(AddClientRequest.Password));
        }
    }
}
