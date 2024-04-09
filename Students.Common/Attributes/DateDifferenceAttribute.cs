using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students.Common.Attributes
{
    public class DateDifferenceAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime startDate = (DateTime)value;
                DateTime endDate = (DateTime)validationContext.ObjectType.GetProperty("EndDate").GetValue(validationContext.ObjectInstance, null);

                if ((endDate - startDate).TotalDays < 4 * 30)
                {
                    return new ValidationResult($"Dates must differ by at least 4 months.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
