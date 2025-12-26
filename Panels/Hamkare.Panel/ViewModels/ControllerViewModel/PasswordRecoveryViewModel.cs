using Hamkare.Resource;
using Hamkare.Utility.Attributes.Core;
using System.ComponentModel.DataAnnotations;

namespace Hamkare.Panel.ViewModels.ControllerViewModel;

public class PasswordRecoveryViewModel
{
    [CoreEmail]
    [Display(Name = nameof(GlobalResources.Email), ResourceType = typeof(GlobalResources))]
    public string Email { get; set; }

    public string ReturnUrl { get; set; }
}