using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Students.Common.Attributes
{
    public class SubjectNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string subjectName = value.ToString();
                if (char.IsLower(subjectName[0]) || Regex.IsMatch(subjectName, @"\d"))
                {
                    return new ValidationResult("Item name cannot contain numbers and cannot begin with a lowercase letter.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
