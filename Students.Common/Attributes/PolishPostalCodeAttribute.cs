using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students.Common.Attributes
{
    public class PolishPostalCodeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string postalCode = value.ToString();
                if (!IsValidPolishPostalCode(postalCode))
                {
                    return new ValidationResult("Почтовый индекс должен быть в формате XX-XXX.");
                }
            }
            return ValidationResult.Success;
        }

        // Method to check if the entered postal code corresponds to the Polish format
        private bool IsValidPolishPostalCode(string postalCode)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(postalCode, @"\b\d{2}-\d{3}\b");
        }
    }
}
