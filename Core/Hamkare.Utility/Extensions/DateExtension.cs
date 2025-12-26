using System.Globalization;

namespace Hamkare.Utility.Extensions;

public static class DateExtension
{
    public static string MiladiToShamsi(this DateTime dateTime)
    {
        var pc = new PersianCalendar();

        return $"{pc.GetYear(dateTime)}-{pc.GetMonth(dateTime)}-{pc.GetDayOfMonth(dateTime)}";
    }

    public static DateTime ShamsiToMiladi(this string dateTimeString)
    {
        var datetime = DateTime.Parse(dateTimeString);

        return new DateTime(datetime.Year, datetime.Month, datetime.Day,datetime.Hour, datetime.Minute,datetime.Second, new PersianCalendar());
    }

    public static long ConvertToUnix(this DateTime dateTime)
    {
        return (long) (dateTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
    }

    public static DateTime RoundUp(this DateTime dateTime)
    {
        var minute = dateTime.Minute;

        var retDateTime = dateTime;

        switch (minute / 15)
        {
            case 0:
                retDateTime = new DateTime(dateTime.Year, dateTime.Month,
                dateTime.Day, dateTime.Hour, 0, 0);
                break;
            case 1:
                retDateTime = new DateTime(dateTime.Year, dateTime.Month,
                dateTime.Day, dateTime.Hour, 30, 0);
                break;
            case 2:
                retDateTime = new DateTime(dateTime.Year, dateTime.Month,
                dateTime.Day, dateTime.Hour, 30, 0);
                break;
            case 3:
                retDateTime = new DateTime(dateTime.Year, dateTime.Month,
                dateTime.Day, dateTime.Hour, 0, 0).AddHours(1);
                break;
        }

        return retDateTime;
    }

    public static TimeSpan RoundUp(this TimeSpan dateTime)
    {
        var minute = dateTime.Minutes;

        var retDateTime = dateTime;

        switch (minute / 15)
        {
            case 0:
                retDateTime = new TimeSpan(dateTime.Days, dateTime.Hours,
                0, 0);
                break;
            case 1:
                retDateTime = new TimeSpan(dateTime.Days, dateTime.Hours,
                30, 0);
                break;
            case 2:
                retDateTime = new TimeSpan(dateTime.Days, dateTime.Hours,
                30, 0);
                break;
            case 3:
                retDateTime = new TimeSpan(dateTime.Days, dateTime.Hours + 1,
                0, 0);
                break;
        }

        return retDateTime;
    }

    public static string ToString(this DateTime dateTime, bool time)
    {
        return time ? dateTime.ToString(Thread.CurrentThread.CurrentUICulture.IsRtl() ? "yyyy/MM/dd HH:mm:ss" : "MM/dd/yyyy HH:mm:ss", Thread.CurrentThread.CurrentUICulture) : dateTime.ToString(Thread.CurrentThread.CurrentUICulture.IsRtl() ? "yyyy/MM/dd" : "MM/dd/yyyy", Thread.CurrentThread.CurrentUICulture);
    }
    
    public static string ToString(this DateTime? dateTime, bool time)
    {
        return dateTime.HasValue ? time ? dateTime.Value.ToString(Thread.CurrentThread.CurrentUICulture.IsRtl() ? "yyyy/MM/dd HH:mm:ss" : "MM/dd/yyyy HH:mm:ss", Thread.CurrentThread.CurrentUICulture) : dateTime.Value.ToString(Thread.CurrentThread.CurrentUICulture.IsRtl() ? "yyyy/MM/dd" : "MM/dd/yyyy", Thread.CurrentThread.CurrentUICulture) : string.Empty;
    }
    
    public static string ToString(this DateOnly dateTime, bool word)
    {
        return dateTime.ToString(Thread.CurrentThread.CurrentUICulture.IsRtl() ? "yyyy/MM/dd" : "MM/dd/yyyy", Thread.CurrentThread.CurrentUICulture);
    }
    
    public static string ToString(this DateOnly? dateTime, bool word)
    {
        return dateTime.HasValue ? dateTime.Value.ToString(Thread.CurrentThread.CurrentUICulture.IsRtl() ? "yyyy/MM/dd" : "MM/dd/yyyy", Thread.CurrentThread.CurrentUICulture) : string.Empty;
    }
    
    public static bool IsRtl(this CultureInfo cultureInfo)
    {
        return cultureInfo.Name == "fa" || cultureInfo.Name == "ar";
    }
}