using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace AFI.API.Common.ValidationProvider
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class RequiredIfFieldIsEmptyAttribute : ValidationAttribute
    {
        private string _otherPropertyName;

        public RequiredIfFieldIsEmptyAttribute(string otherPropertyName)
        {
            _otherPropertyName = otherPropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (IsFieldIsNotBlank(value)) return ValidationResult.Success;

            if (string.IsNullOrWhiteSpace(_otherPropertyName))
            {
                throw new ArgumentException($"Other property name is empty");
            }

            PropertyInfo otherPropertyInfo = validationContext.ObjectType.GetProperty(_otherPropertyName);

            if (otherPropertyInfo == null)
            {
                throw new ArgumentException($"The object does not contain any property with name '{_otherPropertyName}'");
            }

            object otherPropertyContextValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);

            if (IsFieldIsNotBlank(otherPropertyContextValue)) return ValidationResult.Success;

            string formattedErrorMessage = string.Format(CultureInfo.InvariantCulture, ErrorMessage, validationContext.DisplayName);
            return new ValidationResult(formattedErrorMessage);
        }

        private static bool IsFieldIsNotBlank(object otherPropertyContextValue)
        {
            bool isOtherFieldBlank = false;

            Type type = otherPropertyContextValue.GetType();

            if (type == typeof(DateTime))
            {
                if (otherPropertyContextValue != null)
                {
                    if (DateTime.TryParse(otherPropertyContextValue.ToString(), out DateTime outDateTime))
                    {
                        isOtherFieldBlank = outDateTime != DateTime.MinValue;
                    }
                    else
                    {
                        throw new FormatException("The string was not recognized as a valid DateTime. String format should be: 01-Jan-2020 or 01-Jan-2020 10:00:00 AM");
                    }
                }
            }
            else if (type == typeof(string))
            {
                isOtherFieldBlank = !string.IsNullOrWhiteSpace(otherPropertyContextValue?.ToString());
            }

            return isOtherFieldBlank;
        }
    }
}
