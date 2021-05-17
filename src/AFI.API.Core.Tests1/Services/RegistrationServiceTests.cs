using AFI.API.Core.ServiceModels;
using AFI.API.MappingProfiles;
using AFI.Data.Entities;
using AFI.Data.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace AFI.API.Core.Services.Tests
{
    [TestClass()]
    public class RegistrationServiceTests
    {
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


        [DataRow(true, 0, StatusCodes.Status400BadRequest)]
        [DataRow(false, 12345, StatusCodes.Status200OK)]
        [DataRow(false, 0, StatusCodes.Status503ServiceUnavailable)]
        [DataTestMethod]
        public async Task RegisterTest(bool isPolicyRegistered, int customerId, int expectedResult)
        {

            CustomerServiceModel customer = new() { FirstName = "Test", Surname = "Test", DoB = DateTime.Now.AddYears(-18), PolicyReference = "AF-123456" };
            CustomerEntity entity = _mapper.Map<CustomerEntity>(customer);
            entity.CustomerId = customerId;
            var mockCustomerRepo = new Mock<IRegistrationRepository>();
            mockCustomerRepo.Setup(x => x.IsPolicyAlreadyRegistered(It.IsAny<CustomerEntity>())).Returns(Task.FromResult(isPolicyRegistered));
            mockCustomerRepo.Setup(x => x.AddAsync(It.IsAny<CustomerEntity>())).Returns(Task.FromResult(entity));
            var registrationService = new RegistrationService(mockCustomerRepo.Object, _mapper);

            var result = await registrationService.Register(customer);

            Assert.AreEqual(expectedResult, Convert.ToInt32(result.StatusCode));
            Assert.AreEqual(customerId, result.Result);
        }

        [TestMethod]
        [ExpectedException(typeof(TimeoutException), "Database failure")]
        public async Task TestException()
        {

            CustomerServiceModel customer = new() { FirstName = "Test", Surname = "Test", DoB = DateTime.Now.AddYears(-18), PolicyReference = "AF-123456" };
            CustomerEntity entity = _mapper.Map<CustomerEntity>(customer);
            entity.CustomerId = 0;
            var mockCustomerRepo = new Mock<IRegistrationRepository>();
            mockCustomerRepo.Setup(x => x.IsPolicyAlreadyRegistered(It.IsAny<CustomerEntity>())).Returns(Task.FromResult(false));
            mockCustomerRepo.Setup(x => x.AddAsync(It.IsAny<CustomerEntity>())).Throws(new TimeoutException());
            var registrationService = new RegistrationService(mockCustomerRepo.Object, _mapper);
            var result = await registrationService.Register(customer);

        }
    }
}