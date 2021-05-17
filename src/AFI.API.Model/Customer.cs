using AFI.API.Common.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace AFI.API.Model
{
    public class Customer
    {
        [Required]
        [StringLength(Constants.NAMEMAXLENGTH, MinimumLength = Constants.NAMEMINLENGTH)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(Constants.NAMEMAXLENGTH, MinimumLength = Constants.NAMEMINLENGTH)]
        public string Surname { get; set; }

        [Required]
        [RegularExpression(Constants.POLICYREFERENCEFORMAT, ErrorMessage = "Please provide the correct policy reference number.")]
        public string PolicyReference { get; set; }

        [DataType(DataType.Date)]
        public DateTime DoB { get; set; }

        [RegularExpression(Constants.EMAILADDRESSFORMAT)]
        public string EmailAddress { get; set; }
    }
}
