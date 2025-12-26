namespace Hamkare.Utility.Helpers;

public static class ControllerHelpers
{
    public static string GetControllerName(string className)
    {
        var place = className.LastIndexOf("Controller", StringComparison.Ordinal);

        return place == -1 ? className : className.Remove(place, "Controller".Length);
    }
}