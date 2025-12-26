using Hamkare.Resource;
using System.ComponentModel.DataAnnotations;
using Hamkare.Utility.Extensions;

namespace Hamkare.Utility.Attributes.Number;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class NumberNotLessThenAttribute : ValidationAttribute
{
    private readonly long _min;
    private readonly string _otherPropertyName;

    public NumberNotLessThenAttribute(long min)
    {
        _min = min;
        ErrorMessage = ErrorResources.NumberNotGreaterThen;
    }

    public NumberNotLessThenAttribute(string otherPropertyName)
    {
        _otherPropertyName = otherPropertyName;
        ErrorMessage = ErrorResources.NumberNotGreaterThen;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(_otherPropertyName))
        {
            if (value is long number && number < _min)
                return new ValidationResult(string.Format(ErrorMessage, validationContext.DisplayName, _otherPropertyName));
        }
        else
        {
            var otherPropertyInfo = validationContext.ObjectType.GetProperty(_otherPropertyName);

            if (otherPropertyInfo == null)
                return new ValidationResult(string.Format(ErrorResources.UnknownProperty, _otherPropertyName, validationContext.ObjectType.GetClassDisplayName()));

            if (value is not long number || otherPropertyInfo.GetValue(validationContext.ObjectInstance) is not long otherPropertyValue || number > otherPropertyValue)
                return ValidationResult.Success;

            var currentPropertyDisplayName = validationContext.ObjectType.GetPropertyDisplayName(validationContext.MemberName);
            var otherPropertyDisplayName = validationContext.ObjectType.GetPropertyDisplayName(_otherPropertyName);

            return new ValidationResult(string.Format(ErrorMessage, currentPropertyDisplayName, otherPropertyDisplayName));
        }

        return ValidationResult.Success;
    }
}