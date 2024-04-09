using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students.Common.Attributes
{
    public class NoLowerCaseAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string stringValue = value.ToString();
                if (char.IsLower(stringValue[0]))
                {
                    return new ValidationResult("First or last name must not begin with a small letter.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
