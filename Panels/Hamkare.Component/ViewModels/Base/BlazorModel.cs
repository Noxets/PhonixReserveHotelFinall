using Microsoft.JSInterop;

namespace Hamkare.Component.ViewModels.Base;

public class BlazorModel<TBlazorModel> where TBlazorModel : class
{
    public DotNetObjectReference<TBlazorModel> DotNetObject { get; set; }
}