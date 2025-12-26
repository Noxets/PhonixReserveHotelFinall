using Hamkare.Resource;
using Hamkare.Resource.Identity;
using Hamkare.Utility.Attributes.Core;
using System.ComponentModel.DataAnnotations;

namespace Hamkare.Panel.ViewModels.ControllerViewModel;

public class ChangePasswordViewModel
{
    [CoreEmail]
    [Display(Name = nameof(GlobalResources.Email), ResourceType = typeof(GlobalResources))]
    public string Email { get; set; }

    public string Token { get; set; }

    [Display(Name = nameof(GlobalResources.Password), ResourceType = typeof(GlobalResources))]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Compare(nameof(Password), ErrorMessageResourceName = nameof(IdentityResources.ErrorPasswordCompare), ErrorMessageResourceType = typeof(IdentityResources))]
    [Display(Name = nameof(IdentityResources.ConfirmPassword), ResourceType = typeof(IdentityResources))]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
}