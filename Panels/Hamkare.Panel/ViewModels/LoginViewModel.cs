using Hamkare.Resource;
using Hamkare.Resource.Identity;
using Hamkare.Utility.Attributes.Core;
using System.ComponentModel.DataAnnotations;

namespace Hamkare.Panel.ViewModels;

public class LoginViewModel
{
    [Display(Name = nameof(GlobalResources.MobileNumber), ResourceType = typeof(GlobalResources))]
    public string PhoneNumber { get; set; }

    [Display(Name = nameof(GlobalResources.Username), ResourceType = typeof(GlobalResources))]
    public string UserName { get; set; }

    [Display(Name = nameof(GlobalResources.Password), ResourceType = typeof(GlobalResources))]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public string ReturnUrl { get; set; }

    [Display(Name = nameof(IdentityResources.RememeberMe), ResourceType = typeof(IdentityResources))]
    public bool IsPersistent { get; set; } = true;

    [CoreRequired]
    [Display(Name = nameof(GlobalResources.Token), ResourceType = typeof(GlobalResources))]
    public string Token { get; set; }
}