using Hamkare.Common.Enums;

namespace Hamkare.Panel.ViewModels.ControllerViewModel;

public class ErrorViewModel
{
    public ToastType ToastType { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }
}