using Microsoft.VisualStudio.TestTools.UnitTesting;
using AFI.API.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AFI.API.MappingProfiles;

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

        [TestMethod()]
        public void RegisterTest()
        {
            throw new NotImplementedException();
        }
    }
}