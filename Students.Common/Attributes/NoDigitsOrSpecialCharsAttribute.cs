using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Students.Common.Attributes
{
    public class NoDigitsOrSpecialCharsAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string stringValue = value.ToString();
                if (Regex.IsMatch(stringValue, @"\d") || Regex.IsMatch(stringValue, @"[^\w\s]") || stringValue.Split(' ').Length > 2)
                {
                    return new ValidationResult("First and last names cannot contain numbers, special characters, or more than one space.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
