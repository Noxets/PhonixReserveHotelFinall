using Hamkare.Resource;

namespace Hamkare.Utility.Attributes.Core;

public class CoreMaxLengthAttribute(int length) : System.ComponentModel.DataAnnotations.MaxLengthAttribute(length)
{

    public override string FormatErrorMessage(string name)
    {
        return string.Format(ErrorResources.MaxLength, name, Length);
    }
}