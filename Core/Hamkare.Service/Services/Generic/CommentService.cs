using Hamkare.Common.Interface.Entities;
using Hamkare.Common.Interface.Repositories;
using Hamkare.Utility.Extensions;
using Hamkare.Utility.Sender.Sms;
using Hamkare.Utility.Settings.CoreSettings;
using Microsoft.EntityFrameworkCore;

namespace Hamkare.Service.Services.Generic;

public class CommentService<TCommentEntity, TDbContext>(ICommentRepository<TCommentEntity, TDbContext> repository)
    : RootService<TCommentEntity, TDbContext>(repository)
    where TCommentEntity : ICommentEntity, new() where TDbContext : DbContext
{
    public override async Task AddOrUpdateAsync(TCommentEntity newItem, CancellationToken cancellationToken = default)
    {
        await base.AddOrUpdateAsync(newItem, cancellationToken);

        if (SmsSettings.CommentReceiversList.Count != 0 && SmsSettings.CommentTemplate != 0)
            foreach (var phoneNumber in SmsSettings.CommentReceiversList)
                _ = SmsSender.SendSmsJobAsync(phoneNumber, new Dictionary<string, string>
                {
                    {
                        "Name", typeof(TCommentEntity).GetClassDisplayName()
                    },
                    {
                        "EntityType", typeof(TCommentEntity).Name
                    },
                    {
                        "EntityId", newItem.Id.ToString()
                    }
                }, Convert.ToInt32(SmsSettings.CommentTemplate));
    }
}