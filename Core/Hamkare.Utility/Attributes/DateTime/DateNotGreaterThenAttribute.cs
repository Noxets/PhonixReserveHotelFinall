using Hamkare.Resource;
using System.ComponentModel.DataAnnotations;
using Hamkare.Utility.Extensions;

namespace Hamkare.Utility.Attributes.DateTime;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class DateNotGreaterThenAttribute : ValidationAttribute
{
    private readonly System.DateTime _maxDate;
    private readonly string _otherPropertyName;

    public DateNotGreaterThenAttribute(System.DateTime maxDate)
    {
        _maxDate = maxDate;
        ErrorMessage = ErrorResources.DateNotGreaterThen;
    }

    public DateNotGreaterThenAttribute()
    {
        _maxDate = System.DateTime.Now;
        ErrorMessage = ErrorResources.DateNotGreaterThenCurrent;
    }

    public DateNotGreaterThenAttribute(string otherPropertyName)
    {
        _otherPropertyName = otherPropertyName;
        ErrorMessage = ErrorResources.DateNotGreaterThen;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(_otherPropertyName))
        {
            if (value is System.DateTime date && date > _maxDate)
                return new ValidationResult(string.Format(ErrorMessage, validationContext.DisplayName, _otherPropertyName));
        }
        else
        {
            var otherPropertyInfo = validationContext.ObjectType.GetProperty(_otherPropertyName);

            if (otherPropertyInfo == null)
                return new ValidationResult(string.Format(ErrorResources.UnknownProperty, _otherPropertyName, validationContext.ObjectType.GetClassDisplayName()));

            if (value is not System.DateTime date || otherPropertyInfo.GetValue(validationContext.ObjectInstance) is not System.DateTime otherPropertyValue || date < otherPropertyValue)
                return ValidationResult.Success;

            var currentPropertyDisplayName = validationContext.ObjectType.GetPropertyDisplayName(validationContext.MemberName);
            var otherPropertyDisplayName = validationContext.ObjectType.GetPropertyDisplayName(_otherPropertyName);

            return new ValidationResult(string.Format(ErrorMessage, currentPropertyDisplayName, otherPropertyDisplayName));
        }

        return ValidationResult.Success;
    }
}