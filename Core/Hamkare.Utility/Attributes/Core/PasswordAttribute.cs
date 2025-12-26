using Hamkare.Resource.Identity;
using System.ComponentModel.DataAnnotations;

namespace Hamkare.Utility.Attributes.Core;

[AttributeUsage(AttributeTargets.Property)]
public class PasswordAttribute : ValidationAttribute
{
    public PasswordAttribute()
    {
        ErrorMessage = IdentityResources.ErrorPasswordIsNotValid;
    }

    public override bool IsValid(object value)
    {
        var password = value as string;

        if (string.IsNullOrEmpty(password))
            return false;

        bool hasDigit = password.Any(char.IsDigit);
        bool hasLower = password.Any(char.IsLower);
        bool hasUpper = password.Any(char.IsUpper);

        return hasDigit && hasLower && hasUpper;
    }
}