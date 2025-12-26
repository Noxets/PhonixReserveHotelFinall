using Hamkare.Resource;
using System.ComponentModel.DataAnnotations;
using Hamkare.Utility.Extensions;

namespace Hamkare.Utility.Attributes.Core;

[AttributeUsage(AttributeTargets.Property)]
public class CoreEmailAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            return ValidationResult.Success;

        var email = value.ToString();
        var displayName = validationContext.ObjectType.GetPropertyDisplayName(validationContext.DisplayName);

        return !email.IsValidEmail() ? new ValidationResult(string.Format(ErrorResources.ValueIsNotValid, displayName)) : ValidationResult.Success;
    }
}