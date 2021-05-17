using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFI.API.Core.ServiceModels
{
    public class CustomerServiceModel
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string PolicyReference { get; set; }
        public DateTime DoB { get; set; }
        public string EmailAddress { get; set; }
    }
}
