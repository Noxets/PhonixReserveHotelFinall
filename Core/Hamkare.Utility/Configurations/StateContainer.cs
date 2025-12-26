namespace Hamkare.Utility.Configurations;

public class StateContainer<T>
{
    private T Value { get; set; }
    public event Action OnStateChange;
    public void SetValue(T value)
    {
        this.Value = value;
        NotifyStateChanged();
    }

    public T GetValue()
    {
        return Value;
    }

    private void NotifyStateChanged() => OnStateChange?.Invoke();
}