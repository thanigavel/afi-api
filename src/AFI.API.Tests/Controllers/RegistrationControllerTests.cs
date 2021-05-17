using AFI.API.Core.ServiceModels;
using AFI.API.Core.Services.Interface;
using AFI.API.MappingProfiles;
using AFI.API.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace AFI.API.Controllers.Tests
{
    [TestClass()]
    public class RegistrationControllerTests
    {
        private RegistrationController _RegistrationController;
        private IMapper _mapper;

        [TestInitialize]
        public void Initialize()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapProvider());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
        }

        [DataRow(HttpStatusCode.OK, 12345)]
        [DataRow(HttpStatusCode.OK, 20000)]
        [DataRow(HttpStatusCode.BadRequest, 0)]
        [DataTestMethod]

        public async Task RegisterTest(HttpStatusCode statusCode, int customerId)
        {
            Customer customer = new() { FirstName = "Test", Surname = "Test", DoB = DateTime.Now.AddYears(-18), PolicyReference = "AF-123456" };
            var mocklogger = new Mock<ILogger>();
            
            var mockRegistrationService = new Mock<IRegistration>();
            var mockRegisterResponse = new Outcome<int>("Test Error", statusCode, customerId);
            mockRegistrationService.Setup(x => x.Register(It.IsAny<CustomerServiceModel>())).Returns(Task.FromResult(mockRegisterResponse));

            _RegistrationController = new RegistrationController(mockRegistrationService.Object, _mapper, mocklogger.Object);
            var result = await _RegistrationController.Register(customer);
            var okresult = result.Result as ObjectResult;

            if(mockRegisterResponse.StatusCode == HttpStatusCode.OK)
                Assert.AreEqual(mockRegisterResponse.Result, okresult.Value);
            else
                Assert.AreEqual("Test Error", okresult.Value);

            Assert.AreEqual(Convert.ToInt32(mockRegisterResponse.StatusCode), okresult.StatusCode);
        }

        [TestMethod]
        public async Task RegisterTestException()
        {
            Customer customer = new() { FirstName = "Test", Surname = "Test", DoB = DateTime.Now.AddYears(-18), PolicyReference = "AF-123456" };
            var mocklogger = new Mock<ILogger>();

            var mockRegistrationService = new Mock<IRegistration>();
            mockRegistrationService.Setup(x => x.Register(It.IsAny<CustomerServiceModel>())).Throws(new Exception());

            _RegistrationController = new RegistrationController(mockRegistrationService.Object, _mapper, mocklogger.Object);
            var result = await _RegistrationController.Register(customer);
            var okresult = result.Result as ObjectResult;

            Assert.AreEqual("Error Occurred", okresult.Value);

            Assert.AreEqual(Convert.ToInt32(HttpStatusCode.InternalServerError), okresult.StatusCode);
        }

        #region Field validation test case
        //Success
        [DataRow("TestFirstName", "Testsurname", "1990-01-01", "AF-123456", "test@test.com", 0, DisplayName = "Success")]
        //Age Test case
        [DataRow("TestFirstName", "Testsurname", "2021-01-01", "AF-123456", "test@test.com", 1, DisplayName = "Age is less than 18")]
        [DataRow("TestFirstName", "Testsurname", "2003-01-01", "AF-123456", "test@test.com", 0, DisplayName = "Age is greater than 18")]
        //Email Address test case
        [DataRow("TestFirstName", "Testsurname", "2003-01-01", "AF-123456", "test@test.co.uk", 0, DisplayName = "Email address Domain is co.uk ")]
        [DataRow("TestFirstName", "Testsurname", "2003-01-01", "AF-123456", "test@test.COM", 0, DisplayName = "Email address Domain is upper case")]
        [DataRow("TestFirstName", "Testsurname", "2003-01-01", "AF-123456", "test@test.co.in", 1, DisplayName = "Email Domain is co.in")]
        [DataRow("TestFirstName", "Testsurname", "2003-01-01", "AF-123456", "t@test.com", 1, DisplayName = "Email address is single character")]
        [DataRow("TestFirstName", "Testsurname", "2003-01-01", "AF-123456", "test@t.com", 1, DisplayName = "Email address Domain is single character")]
        //First Name test case
        [DataRow("23423423", "Testsurname", "1990-01-01", "AF-123456", "test@test.com", 0, DisplayName = "First Name accepts number")]
        [DataRow("DummyDummyDummyDummyDummyDummyDummyDummyDummyDummy", "Testsurname", "1990-01-01", "AF-123456", "test@test.com", 0, DisplayName = "First Name length is 50")]
        [DataRow("TestDummyDummyDummyDummyDummyDummyDummyDummyDummyDummy", "Testsurname", "1990-01-01", "AF-123456", "test@test.com", 1, DisplayName = "First Name length is more than 50")]
        [DataRow("Te", "Testsurname", "1990-01-01", "AF-123456", "test@test.com", 1, DisplayName = "First Name length is less than 3")]
        //Surname test case
        [DataRow("TestFirstName", "23423423", "1990-01-01", "AF-123456", "test@test.com", 0, DisplayName = "surname Name accepts number")]
        [DataRow("TestFirstName", "DummyDummyDummyDummyDummyDummyDummyDummyDummyDummy", "1990-01-01", "AF-123456", "test@test.com", 0, DisplayName = "surname length is 50")]
        [DataRow("TestFirstName", "TestDummyDummyDummyDummyDummyDummyDummyDummyDummyDummy", "1990-01-01", "AF-123456", "test@test.com", 1, DisplayName = "surname length is more than 50")]
        [DataRow("TestFirstName", "Te", "1990-01-01", "AF-123456", "test@test.com", 1, DisplayName = "surname length is less than 3")]
        //policyReference test case
        [DataRow("TestFirstName", "Testsurname", "1990-01-01", "AF-12345A", "test@test.com", 1, DisplayName = "Incorrect policy reference second part format is incorrect")]
        [DataRow("TestFirstName", "Testsurname", "1990-01-01", "A1-123456", "test@test.com", 1, DisplayName = "Incorrect policy reference first part format is incorrect")]
        [DataRow("TestFirstName", "Testsurname", "1990-01-01", "af-123456", "test@test.com", 1, DisplayName = "Incorrect policy first part chars are lower case")]
        [DataRow("TestFirstName", "Testsurname", "1990-01-01", "AF+123456", "test@test.com", 1, DisplayName = "Incorrect policy delimiter char is not '-'")]
        //Either DOb or Email address required
        [DataRow("TestFirstName", "Testsurname", "2000-01-01", "AF-123456", "", 0, DisplayName = "Valid DoB, email address has not passed")]
        [DataRow("TestFirstName", "Testsurname", "", "AF-123456", "test@test.com", 0, DisplayName = "DoB has not passed and valid email address")]
        [DataRow("TestFirstName", "Testsurname", "", "AF-123456", "", 2, DisplayName = "Both Email address and DoB are not passed")]
        [DataTestMethod]
        public void ValidateFieldTest(string firstname, string surname, string DoB, string policyRef, string email, int expectErrorCount)
        {
            Customer customer = new() { FirstName = firstname, Surname = surname, DoB = DateTime.Parse(string.IsNullOrWhiteSpace(DoB) ? DateTime.MinValue.ToShortDateString() : DoB), PolicyReference = policyRef, EmailAddress = email };
            var actualErrorCount = ValidateModel(customer);
            Assert.AreEqual(expectErrorCount, actualErrorCount.Count);
        }

        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(model, null, null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(model, validationContext, results, true);
            return results;
        }

        #endregion
    }
}