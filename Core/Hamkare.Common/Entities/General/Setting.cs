using Hamkare.Common.Entities.Generics;

namespace Hamkare.Common.Entities.General;

public class Setting : RootEntity
{
    public string Category { get; set; }

    public string Key { get; set; }

    public string Value { get; set; }
}