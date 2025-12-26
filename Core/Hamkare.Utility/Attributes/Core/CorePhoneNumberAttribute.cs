using Hamkare.Resource;
using System.ComponentModel.DataAnnotations;
using Hamkare.Utility.Extensions;

namespace Hamkare.Utility.Attributes.Core;

public class CorePhoneNumberAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            return ValidationResult.Success;

        var phoneValue = value.ToString();

        return phoneValue.IsValidPhoneNumber() ? ValidationResult.Success : new ValidationResult(string.Format(ErrorResources.ValueIsNotValid, validationContext.ObjectType.GetPropertyDisplayName(validationContext.MemberName)));
    }
}