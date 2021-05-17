using AFI.API.Core.ServiceModels;
using AFI.API.Core.Services.Interface;
using AFI.Data.Entities;
using AFI.Data.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace AFI.API.Core.Services
{
    public class RegistrationService : IRegistration
    {
        private IRegistrationRepository _customerRepository;
        private IMapper _mapper;

        public RegistrationService(IRegistrationRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;

        }
        public async Task<Outcome<int>> Register(CustomerServiceModel customerServiceModel)
        {
            CustomerEntity customer = _mapper.Map<CustomerEntity>(customerServiceModel);

            bool IsCustomerExist = await _customerRepository.IsPolicyAlreadyRegistered(customer);

            if (IsCustomerExist) 
                return new Outcome<int>(
                     "Policy has already registered, please check the policy reference number", System.Net.HttpStatusCode.BadRequest, 0);

            var savedEntity =  await _customerRepository.AddAsync(customer);

            if (savedEntity?.CustomerId != 0)
            {
                return new Outcome<int>(
                   "Customer registered sucessfully", System.Net.HttpStatusCode.OK, savedEntity.CustomerId);
            }
            else
            {
                return new Outcome<int>(
                    "Please try again, Request has not processed", System.Net.HttpStatusCode.ServiceUnavailable, 0);
            }
        }
    }
}
