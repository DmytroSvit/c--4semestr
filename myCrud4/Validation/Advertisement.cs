using System;
using System.ComponentModel.DataAnnotations;
using myCrud.Models;

namespace myCrud.Validation
{
    public class PriceValidator : ValidationAttribute
    {
        protected override ValidationResult
            IsValid(object? value, ValidationContext validationContext)
        {
            if ((int)value >= 0)
                return ValidationResult.Success;
            else
                return new ValidationResult("Price can not be less than zero.");
        }
    }

    public class StartDateValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            Advertisement ad = validationContext.ObjectInstance as Advertisement;
            DateTime startDate = Convert.ToDateTime(value);
            if (ad.EndDate != null && startDate <= ad.EndDate)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult
                    ("Start date cannot be later than end date");
            }
        }
    }

    public class UrlValidator : ValidationAttribute
    {
        public static bool CheckURLValid(string source) => Uri.TryCreate(source, UriKind.Absolute, out Uri uriResult) && uriResult.Scheme == Uri.UriSchemeHttps;
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (CheckURLValid((string)value))
                return ValidationResult.Success;
            else
                return new ValidationResult
                    ("Not valid url");
        }
    }

    
}
