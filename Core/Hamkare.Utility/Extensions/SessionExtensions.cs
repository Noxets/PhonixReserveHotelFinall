using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Hamkare.Utility.Extensions;

public static class SessionExtensions
{
    public static void Set<T>(this ISession session, string key, T value)
    {
        session.SetString(key, JsonSerializer.Serialize(value));
    }

    public static T Get<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default : JsonSerializer.Deserialize<T>(value);
    }

    public static void Remove(this ISession session, string key)
    {
        session.Remove(key);
    }
    
    #region Basket
    
    public static async Task<List<ShopCartDto>> GetSessionShopCardItemsAsync(this ProtectedSessionStorage protectedSessionStore)
    {
        var result = await protectedSessionStore.GetAsync<List<ShopCartDto>>("ShopCart");
        return result.Value ?? [];
    }
    
    public static async Task<int> GetSessionShopCardItemsCountAsync(this ProtectedSessionStorage protectedSessionStore)
    {
        var result = await GetSessionShopCardItemsAsync(protectedSessionStore);
        return result.Sum(x => x.Count);
    }
    
    public static async Task RemoveSessionShopCardItemsCountAsync(this ProtectedSessionStorage protectedSessionStore)
    {
        await protectedSessionStore.SetAsync("ShopCart", new List<ShopCartDto>());
    }
    
    #region Good
    
    public static async Task AddGoodInSessionShopCardAsync(this ProtectedSessionStorage protectedSessionStore, long goodId, int count)
    {
        if (count < 1)
            await RemoveSkillInSessionShopCardAsync(protectedSessionStore,goodId);
        
        var items = await GetSessionShopCardItemsAsync(protectedSessionStore);
        var goodIndex = items.FindIndex(x => x.GoodId == goodId);
        if (goodIndex != -1)
            items[goodIndex] = new ShopCartDto
            {
                GoodId = goodId,
                Count = count
            };
        else
            items.Add(new ShopCartDto
            {
                GoodId = goodId,
                Count = count
            });

        await protectedSessionStore.SetAsync("ShopCart", items);
    }

    public static async Task RemoveGoodInSessionShopCardAsync(this ProtectedSessionStorage protectedSessionStore, long goodId)
    {
        var items = await GetSessionShopCardItemsAsync(protectedSessionStore);
        if (items.Any(x => x.GoodId == goodId))
        {
            items.Remove(items.FirstOrDefault(x => x.GoodId == goodId));
            await protectedSessionStore.SetAsync("ShopCart", items);
        }
    }
    
    #endregion

    #region Skill

    public static async Task AddSkillInSessionShopCardAsync(this ProtectedSessionStorage protectedSessionStore, long skillId, int count)
    {
        if (count < 1)
            await RemoveSkillInSessionShopCardAsync(protectedSessionStore,skillId);
        
        var items = await GetSessionShopCardItemsAsync(protectedSessionStore);
        var skillIndex = items.FindIndex(x => x.SkillId == skillId);
        if (skillIndex != -1)
            items[skillIndex] = new ShopCartDto
            {
                SkillId = skillId,
                Count = count
            };
        else
            items.Add(new ShopCartDto
            {
                SkillId = skillId,
                Count = count
            });

        await protectedSessionStore.SetAsync("ShopCart", items);
    }

    public static async Task RemoveSkillInSessionShopCardAsync(this ProtectedSessionStorage protectedSessionStore, long skillId)
    {
        var items =await GetSessionShopCardItemsAsync(protectedSessionStore);
        if (items.Any(x => x.SkillId == skillId))
        {
            items.Remove(items.FirstOrDefault(x => x.SkillId == skillId));
            await protectedSessionStore.SetAsync("ShopCart", items);
        }
    }

    #endregion
    
    #region file

    public static async Task AddDownloadInSessionShopCardAsync(this ProtectedSessionStorage protectedSessionStore, long fileId, int count)
    {
        if (count < 1)
            await RemoveFileInSessionShopCardAsync(protectedSessionStore,fileId);
        
        var items = await GetSessionShopCardItemsAsync(protectedSessionStore);
        var skillIndex = items.FindIndex(x => x.FileId == fileId);
        if (skillIndex != -1)
            items[skillIndex] = new ShopCartDto
            {
                FileId = fileId,
                Count = count
            };
        else
            items.Add(new ShopCartDto
            {
                FileId = fileId,
                Count = count
            });

        await protectedSessionStore.SetAsync("ShopCart", items);
    }

    public static async Task RemoveFileInSessionShopCardAsync(this ProtectedSessionStorage protectedSessionStore, long fileId)
    {
        var items =await GetSessionShopCardItemsAsync(protectedSessionStore);
        if (items.Any(x => x.FileId == fileId))
        {
            items.Remove(items.FirstOrDefault(x => x.FileId == fileId));
            await protectedSessionStore.SetAsync("ShopCart", items);
        }
    }

    #endregion

    #endregion
}

public class ShopCartDto
{
    public long GoodId { get; set; }

    public long FileId { get; set; }

    public long SkillId { get; set; }

    public int Count { get; set; }
}