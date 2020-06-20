using AdvertApi.DTOs.Requests;
using AdvertApi.DTOs.Responses;
using AdvertApi.Model;
using AdvertApi.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Tests.UnitTests.Adverts
{
    [TestFixture]
    class AdvertsLoginClientUnitTests
    {
        [Test]
        public void LoginClientMethod_ClientExists_Correct()
        {
            var dbLayer = new Mock<IAdvertDbService>();
            dbLayer.Setup(e => e.LoginClient(new AdvertApi.DTOs.Requests.LoginClientRequest { Login = "login", Password = "Password" }))
                .Returns(new LoginClientResponse { RefreshToken = "refresh", AccessToken = "acces" });
            var cont = new AdvertsController(dbLayer.Object);

            var result = cont.LoginClient(new AdvertApi.DTOs.Requests.LoginClientRequest { Login = "login", Password = "Password" });
            
            Assert.IsNotNull(result);
            Assert.IsTrue(result is OkObjectResult);
        }
        [Test]
        public void LoginClientMethod_NoPassword_Incorrect()
        {
            LoginClientRequest req = new LoginClientRequest { Login = "login" };
            var context = new ValidationContext(req, null, null);
            var results = new List<ValidationResult>();

            var isModelStateValid = Validator.TryValidateObject(req, context, results, true);

            Assert.IsFalse(isModelStateValid);
            Assert.IsTrue(results.Count == 1);
            Assert.IsTrue(results[0].MemberNames.ElementAt(0) == nameof(AddClientRequest.Password));
        }
        [Test]
        public void LoginClientMethod_CompleteRequest_Correct()
        {
            LoginClientRequest req = new LoginClientRequest { Login = "login",Password="password" };
            var context = new ValidationContext(req, null, null);
            var results = new List<ValidationResult>();

            var isModelStateValid = Validator.TryValidateObject(req, context, results, true);

            Assert.IsTrue(isModelStateValid);
            Assert.IsTrue(results.Count == 0);
        }        
        [Test]
        public void LoginClientMethod_PasswordTooShort_Correct()
        {
            LoginClientRequest req = new LoginClientRequest { Login = "login",Password="admin" };
            var context = new ValidationContext(req, null, null);
            var results = new List<ValidationResult>();

            var isModelStateValid = Validator.TryValidateObject(req, context, results, true);

            Assert.IsFalse(isModelStateValid);
            Assert.IsTrue(results.Count == 1);
            Assert.IsTrue(results[0].MemberNames.ElementAt(0) == nameof(AddClientRequest.Password));
        }
    }
}
