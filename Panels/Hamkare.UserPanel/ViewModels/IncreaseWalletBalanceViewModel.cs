using Hamkare.Resource;
using Hamkare.Utility.Attributes.Number;
using System.ComponentModel.DataAnnotations;

namespace Hamkare.UserPanel.ViewModels;

public class IncreaseWalletBalanceViewModel
{
    public long PaymentId { get; set; }

    [NumberNotLessThen(0)]
    [Display(Name = nameof(GlobalResources.Price), ResourceType = typeof(GlobalResources))]
    public decimal Amount { get; set; }
}