using Hamkare.Resource;

namespace Hamkare.Utility.Attributes.Core;

public class CoreRequiredAttribute : System.ComponentModel.DataAnnotations.RequiredAttribute
{
    public override string FormatErrorMessage(string name)
    {
        return string.Format(ErrorResources.Required, name);
    }
}