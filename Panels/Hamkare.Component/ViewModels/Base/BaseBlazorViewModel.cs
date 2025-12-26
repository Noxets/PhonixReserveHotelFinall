
namespace Hamkare.Component.ViewModels.Base;

public class BaseBlazorViewModel<T, TComponent> : BlazorModel<TComponent> where TComponent : class
{
    public T Value { get; set; }
}