using System.Text.RegularExpressions;

namespace Hamkare.Utility.Extensions;

public static class StringExtension
{
    private static readonly Random Random = new Random();
    private const string Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

    public static bool IsValidEmail(this string email)
    {
        if (string.IsNullOrEmpty(email))
            return false;

        var pattern = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";
        return Regex.IsMatch(email, pattern);
    }

    public static bool IsValidPhoneNumber(this string phoneNumber)
    {
        var regex = Thread.CurrentThread.CurrentUICulture.Name switch
        {
            "fa" => new Regex(@"^09[0-9]{9}$"), // Iran
            "ar" => new Regex(@"^9\d{8}$"), // Iraq, Syria, Lebanon, Yemen
            "tr" => new Regex(@"^\+[0-9]{11,12}$"), // Turkey
            _ => new Regex(@"^(\+\d{1,2})?[0-9]{10,11}$")
        };

        return regex.IsMatch(phoneNumber);
    }

    public static string ReplacePersianArabicNumbers(this string number)
    {
        //Arabic
        number = number.Replace('٠', '0');
        number = number.Replace('١', '1');
        number = number.Replace('٢', '2');
        number = number.Replace('٣', '3');
        number = number.Replace('٤', '4');
        number = number.Replace('٥', '5');
        number = number.Replace('٦', '6');
        number = number.Replace('٧', '7');
        number = number.Replace('٨', '8');
        number = number.Replace('٩', '9');

        //Persian
        number = number.Replace('۰', '0');
        number = number.Replace('۱', '1');
        number = number.Replace('۲', '2');
        number = number.Replace('۳', '3');
        number = number.Replace('۴', '4');
        number = number.Replace('۵', '5');
        number = number.Replace('۶', '6');
        number = number.Replace('۷', '7');
        number = number.Replace('۸', '8');
        number = number.Replace('۹', '9');
        return number;
    }

    public static bool IsNumber(this string input)
    {
        return decimal.TryParse(input, out _);
    }

    public static string GenerateRandomString(int length)
    {
        var result = new char[length];
        var lettersLength = Letters.Length;
        for (var i = 0; i < length; i++)
        {
            result[i] = Letters[Random.Next(lettersLength)];
        }

        return new string(result);
    }
}