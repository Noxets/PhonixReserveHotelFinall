namespace Hamkare.Utility.Sender.Sms;

public interface ISmsSender
{
    Task<string> SendSmsAsync(List<string> phoneNumber, List<string> message);

    Task<string> SendSmsAsync(List<string> phoneNumber, string message);

    Task<string> SendSmsAsync(string phoneNumber, List<string> message);

    Task<string> SendSmsAsync(string phoneNumber, string message);

    Task<string> SendVerifyCodeAsync(string phoneNumber, string token);
}