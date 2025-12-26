using Hamkare.Resource;
using System.ComponentModel.DataAnnotations;
using Hamkare.Utility.Extensions;

namespace Hamkare.Utility.Attributes.Number;

[AttributeUsage(AttributeTargets.Property)]
public class NumberAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        switch (value)
        {
            case null:
            case string stringValue when stringValue.IsNumber():
                return ValidationResult.Success;
            default:
                return new ValidationResult(string.Format(ErrorResources.Number, validationContext.ObjectType.GetPropertyDisplayName(validationContext.DisplayName)));
        }
    }
}