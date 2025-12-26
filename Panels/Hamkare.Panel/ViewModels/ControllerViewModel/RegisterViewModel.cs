using Hamkare.Resource;
using Hamkare.Resource.Identity;
using Hamkare.Utility.Attributes.Core;
using System.ComponentModel.DataAnnotations;

namespace Hamkare.Panel.ViewModels.ControllerViewModel;

public class RegisterViewModel : IValidatableObject
{
    [CoreEmail]
    [Display(Name = nameof(GlobalResources.Email), ResourceType = typeof(GlobalResources))]
    public string Email { get; set; }

    [CorePhoneNumber]
    [Display(Name = nameof(GlobalResources.MobileNumber), ResourceType = typeof(GlobalResources))]
    public string PhoneNumber { get; set; }

    [Display(Name = nameof(GlobalResources.Password), ResourceType = typeof(GlobalResources))]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Compare(nameof(Password), ErrorMessageResourceName = nameof(IdentityResources.ErrorPasswordCompare), ErrorMessageResourceType = typeof(IdentityResources))]
    [Display(Name = nameof(IdentityResources.ConfirmPassword), ResourceType = typeof(IdentityResources))]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }

    public string ReturnUrl { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if ((string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(PhoneNumber)) || (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(PhoneNumber)))
            yield return new ValidationResult(IdentityResources.ErrorFillEmailAndPhoneNumberSametime);
    }
}