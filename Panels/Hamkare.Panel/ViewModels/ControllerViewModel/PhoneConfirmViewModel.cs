using Hamkare.Resource;
using Hamkare.Utility.Attributes.Core;
using System.ComponentModel.DataAnnotations;

namespace Hamkare.Panel.ViewModels.ControllerViewModel;

public class PhoneConfirmViewModel
{
    public long UserId { get; set; }

    [CoreRequired]
    [Display(Name = nameof(GlobalResources.MobileNumber), ResourceType = typeof(GlobalResources))]
    public string PhoneNumber { get; set; }

    [CoreRequired]
    [Display(Name = nameof(GlobalResources.Token), ResourceType = typeof(GlobalResources))]
    public string Token { get; set; }

    public string ReturnUrl { get; set; }
}