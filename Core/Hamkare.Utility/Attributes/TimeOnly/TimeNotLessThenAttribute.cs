using Hamkare.Resource;
using System.ComponentModel.DataAnnotations;
using Hamkare.Utility.Extensions;

namespace Hamkare.Utility.Attributes.TimeOnly;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class TimeNotLessThenAttribute : ValidationAttribute
{
    private readonly System.TimeOnly _maxTime;
    private readonly string _otherPropertyName;

    public TimeNotLessThenAttribute(System.TimeOnly maxTime)
    {
        _maxTime = maxTime;
        ErrorMessage = ErrorResources.TimeNotLessThen;
    }

    public TimeNotLessThenAttribute()
    {
        _maxTime = new System.TimeOnly(System.DateTime.Now.Hour, System.DateTime.Now.Minute, System.DateTime.Now.Second);
        ErrorMessage = ErrorResources.TimeNotLessThenCurrent;
    }

    public TimeNotLessThenAttribute(string otherPropertyName)
    {
        _otherPropertyName = otherPropertyName;
        ErrorMessage = ErrorResources.TimeNotLessThen;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(_otherPropertyName))
        {
            if (value is System.TimeOnly time && time < _maxTime)
                return new ValidationResult(string.Format(ErrorMessage, validationContext.DisplayName, _otherPropertyName));
        }
        else
        {
            var otherPropertyInfo = validationContext.ObjectType.GetProperty(_otherPropertyName);

            if (otherPropertyInfo == null)
                return new ValidationResult(string.Format(ErrorResources.UnknownProperty, _otherPropertyName, validationContext.ObjectType.GetClassDisplayName()));

            if (value is not System.TimeOnly time || otherPropertyInfo.GetValue(validationContext.ObjectInstance) is not System.TimeOnly otherPropertyValue || time > otherPropertyValue)
                return ValidationResult.Success;

            var currentPropertyDisplayName = validationContext.ObjectType.GetPropertyDisplayName(validationContext.MemberName);
            var otherPropertyDisplayName = validationContext.ObjectType.GetPropertyDisplayName(_otherPropertyName);

            return new ValidationResult(string.Format(ErrorMessage, currentPropertyDisplayName, otherPropertyDisplayName));
        }

        return ValidationResult.Success;
    }
}