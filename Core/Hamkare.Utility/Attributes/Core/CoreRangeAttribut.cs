using Hamkare.Resource;
using System.ComponentModel.DataAnnotations;
using Hamkare.Utility.Extensions;

namespace Hamkare.Utility.Attributes.Core;

public class CoreRangeAttribute : RangeAttribute
{
    public CoreRangeAttribute(double minimum, double maximum) : base(minimum, maximum)
    {
    }

    public CoreRangeAttribute(int minimum, int maximum) : base(minimum, maximum)
    {
    }

    public CoreRangeAttribute(Type type, string minimum, string maximum) : base(type, minimum, maximum)
    {
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        ErrorMessage = string.Format(ErrorResources.Range, validationContext.ObjectType.GetPropertyDisplayName(validationContext.MemberName));
        return base.IsValid(value, validationContext);
    }
}