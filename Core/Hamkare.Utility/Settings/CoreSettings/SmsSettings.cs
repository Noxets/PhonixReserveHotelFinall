using Hamkare.Utility.Attributes.Core;

namespace Hamkare.Utility.Settings.CoreSettings;

public class SmsSettings
{
    public static string Provider { get; set; }

    public static string Token { get; set; }

    public static long Number { get; set; }

    public static int LoginTemplate { get; set; }

    public static int CommentTemplate { get; set; }
    
    public static string CommentReceivers { get; set; }

    [NotUpdateSetting]
    public static List<string> CommentReceiversList => string.IsNullOrWhiteSpace(CommentReceivers) ? [] : CommentReceivers.Split(",").ToList();
    
    public static string SmsList { get; set; }
}