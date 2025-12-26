namespace Hamkare.Component.ViewModels.Panel;

public class DropdownViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }

    // Note: this is important so the select can compare pizzas
    public override bool Equals(object o)
    {
        var other = o as DropdownViewModel;
        return other?.Id == Id;
    }

    // Note: this is important so the select can compare pizzas
    public override int GetHashCode() => Id.GetHashCode();
}