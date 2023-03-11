using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetLab202303Hosted.Shared.CustomValidate
{
    public class BirthdayValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime)
            {
                var dateValue = (DateTime)value;
                if (dateValue < DateTime.Now)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(ErrorMessage);
                }
            }
            throw new ArgumentException("引数の型が違います", nameof(value));
        }

    }
}
