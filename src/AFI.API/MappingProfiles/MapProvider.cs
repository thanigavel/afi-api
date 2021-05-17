using AFI.API.Core.ServiceModels;
using AFI.API.Model;
using AFI.Data.Entities;
using AutoMapper;

namespace AFI.API.MappingProfiles
{
    public class MapProvider : Profile
    {
        public MapProvider()
        {
            this.CreateMap<Customer, CustomerServiceModel>();
            this.CreateMap<CustomerServiceModel, CustomerEntity>();

        }
    }
}
