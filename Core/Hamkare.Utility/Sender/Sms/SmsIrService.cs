using Hamkare.Utility.Settings.CoreSettings;
using IPE.SmsIrClient;
using IPE.SmsIrClient.Models.Requests;
using Hamkare.Utility.Extensions;

namespace Hamkare.Utility.Sender.Sms;

public class SmsIrService
{
    private readonly SmsIr _smsIr = new(SmsSettings.Token);

    public async Task<string> Send(string message, string phoneNumber, DateTime? sendTime = null)
    {
        var bulkSendResult = sendTime == null
            ? await _smsIr.BulkSendAsync(SmsSettings.Number, message, [
                phoneNumber.ReplacePersianArabicNumbers()
            ])
            : await _smsIr.BulkSendAsync(SmsSettings.Number, message, [
                phoneNumber.ReplacePersianArabicNumbers()
            ], (int?) sendTime.Value.ConvertToUnix());
        return bulkSendResult.Message;
    }

    public async Task<string> Send(string message, List<string> phoneNumber, DateTime? sendTime = null)
    {
        var bulkSendResult = sendTime == null ? await _smsIr.BulkSendAsync(SmsSettings.Number, message, phoneNumber.Select(x => x.ReplacePersianArabicNumbers()).ToArray()) : await _smsIr.BulkSendAsync(SmsSettings.Number, message, phoneNumber.ToArray(), (int?) sendTime.Value.ConvertToUnix());
        return bulkSendResult.Message;
    }

    public async Task<string> SendWithTemplate(Dictionary<string, string> messageContent, int templateId, string phoneNumber)
    {
        var verificationSendResult = await _smsIr.VerifySendAsync(phoneNumber.ReplacePersianArabicNumbers(), templateId, messageContent.Select(x => new VerifySendParameter(x.Key, x.Value)).ToArray());
        return verificationSendResult.Message;
    }
}