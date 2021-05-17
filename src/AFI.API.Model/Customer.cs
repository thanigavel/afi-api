using System;
using System.ComponentModel.DataAnnotations;

namespace AFI.API.Model
{
    public class Customer
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string PolicyReference { get; set; }

        [DataType(DataType.Date)]
        public DateTime DoB { get; set; }

        public string EmailAddress { get; set; }
    }
}
