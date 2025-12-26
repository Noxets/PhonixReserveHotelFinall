namespace Hamkare.Utility.Settings.CoreSettings;

public class GlobalSettings
{
    public static bool CanRegister { get; set; }

    public static int UploadLimit { get; set; } = 512000;

    public static string Header { get; set; }
    
    public static string OwnerName { get; set; }
    
    public static string PhoneNumber { get; set; }

    public static string Body { get; set; }
    
    public static string CurrencyLanguage { get; set; }
}