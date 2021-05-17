using System;
using System.ComponentModel.DataAnnotations;

namespace AFI.Data.Entities
{
    public class CustomerEntity 
    {
        [Key]
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string PolicyReference { get; set; }
        public DateTime DoB { get; set; }
        public string EmailAddress { get; set; }
    }
}
