using Hamkare.Resource;
using Hamkare.Utility.Extensions;
using Hamkare.Utility.Settings.CoreSettings;

namespace Hamkare.Utility.Sender.Sms;

public static class SmsSender
{
    public static Task<string> SendSmsAsync(List<string> phoneNumber, List<string> message)
    {
        return Task.FromResult("");
    }

    public static Task<string> SendSmsAsync(List<string> phoneNumber, string message)
    {
        return Task.FromResult("");
    }

    public static Task<string> SendSmsAsync(string phoneNumber, List<string> message)
    {
        return Task.FromResult("");
    }

    public static async Task<string> SendSmsAsync(string phoneNumber, string message)
    {
        switch (SmsSettings.Provider.ToEnum<SmsProviders>())
        {
            case SmsProviders.SmsIr:

                var service = new SmsIrService();

                return await service.Send(message, phoneNumber);

            default:
                throw new Exception(ErrorResources.SmsServiceNotFind);
        }
    }

    public static async Task<string> SendSmsJobAsync(string phoneNumber, Dictionary<string, string> messageContent, int templateNumber)
    {
        switch (SmsSettings.Provider.ToEnum<SmsProviders>())
        {
            case SmsProviders.SmsIr:

                var service = new SmsIrService();

                return await service.SendWithTemplate(messageContent, templateNumber, phoneNumber);

            default:
                throw new Exception(ErrorResources.SmsServiceNotFind);
        }
    }

    public static async Task<string> SendVerifyCodeAsync(string phoneNumber, string token)
    {
        switch (SmsSettings.Provider.ToEnum<SmsProviders>())
        {
            case SmsProviders.SmsIr:

                var service = new SmsIrService();

                return await service.SendWithTemplate(new Dictionary<string, string>
                {
                    {
                        "Token", token
                    }
                }, SmsSettings.LoginTemplate, phoneNumber);

            default:
                throw new Exception(ErrorResources.SmsServiceNotFind);
        }
    }
}