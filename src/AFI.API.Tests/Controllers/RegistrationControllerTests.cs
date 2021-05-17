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
    }
}