using AdvertApi.DTOs.Requests;
using AdvertApi.DTOs.Responses;
using AdvertApi.Model;
using AdvertApi.Models;
using AdvertApi.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Tests.UnitTests.Adverts
{
    [TestFixture]
    class AdvertsAddClientsUnitTests
    {
        [Test]
        public void AddClientMethod_CompleteRequest_Correct()
        {
            AddClientRequest req = new AddClientRequest { FirstName = "Fname", LastName = "Lname", Email = "Email", Phone = "Phone", Login = "Login", Password = "zaq1@WSX" };
            var context = new ValidationContext(req, null, null);
            var results = new List<ValidationResult>();

            var isModelStateValid = Validator.TryValidateObject(req, context, results, true);

            Assert.IsTrue(isModelStateValid);
            Assert.IsTrue(results.Count == 0);
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
