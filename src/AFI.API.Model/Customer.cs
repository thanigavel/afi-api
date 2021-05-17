using AFI.API.Common.Constants;
using AFI.API.Common.ValidationProvider;
using System;
using System.ComponentModel.DataAnnotations;
using TanvirArjel.CustomValidation.Attributes;

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
        [MinAge(Constants.MINIMUMAGEINYEARS, 0, 0, ErrorMessage = "Minimum age should be 18 years")]
        [RequiredIfFieldIsEmpty("EmailAddress", ErrorMessage = "Either DoB or EmailAddress field must be provided")]
        public DateTime DoB { get; set; }

        [RegularExpression(Constants.EMAILADDRESSFORMAT)]
        [RequiredIfFieldIsEmpty("DoB", ErrorMessage = "Either DoB or EmailAddress field must be provided")]
        public string EmailAddress { get; set; }
    }
}
