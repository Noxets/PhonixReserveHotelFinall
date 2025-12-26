using Hamkare.Utility.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Hamkare.Panel.Areas.Api.Controllers;

[IgnoreAntiforgeryToken]
[Route("/Api/[controller]/[action]")]
public class ApiController : Controller
{
    protected List<ShopCartDto> GetSessionShopCardItems()
    {
        return HttpContext.Session.Get<List<ShopCartDto>>("ShopCart") ?? [];
    }

    protected int GetSessionShopCardItemsCount()
    {
        return GetSessionShopCardItems().Sum(x => x.Count);
    }

    protected void RemoveSessionShopCardItemsCount()
    {
        HttpContext.Session.Set("ShopCart", new List<ShopCartDto>());
    }

    protected void AddGoodInSessionShopCard(long goodId, int count)
    {
        var items = GetSessionShopCardItems();
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

        HttpContext.Session.Set("ShopCart", items);
    }

    protected void RemoveGoodInSessionShopCard(long goodId)
    {
        var items = GetSessionShopCardItems();
        items.Remove(items.FirstOrDefault(x => x.GoodId == goodId));
        HttpContext.Session.Set("ShopCart", items);
    }

    #region Skill

    protected void AddSkillInSessionShopCard(long serviceId, int count)
    {
        var items = GetSessionShopCardItems();
        var skillIndex = items.FindIndex(x => x.SkillId == serviceId);
        if (skillIndex != -1)
            items[skillIndex] = new ShopCartDto
            {
                SkillId = serviceId,
                Count = count
            };
        else
            items.Add(new ShopCartDto
            {
                SkillId = serviceId,
                Count = count
            });

        HttpContext.Session.Set("ShopCart", items);
    }

    protected void RemoveSkillInSessionShopCard(long serviceId)
    {
        var items = GetSessionShopCardItems();
        items.Remove(items.FirstOrDefault(x => x.SkillId == serviceId));
        HttpContext.Session.Set("ShopCart", items);
    }

    #endregion

    #region Download

    protected void AddDownloadInSessionShopCard(long downloadId)
    {
        var items = GetSessionShopCardItems();
        var downloadIndex = items.FindIndex(x => x.FileId == downloadId);
        if (downloadIndex != -1)
            return;

        items.Add(new ShopCartDto
        {
            FileId = downloadId,
            Count = 1
        });
        HttpContext.Session.Set("ShopCart", items);
    }

    protected void RemoveDownloadInSessionShopCard(long downloadId)
    {
        var items = GetSessionShopCardItems();
        items.Remove(items.FirstOrDefault(x => x.FileId == downloadId));
        HttpContext.Session.Set("ShopCart", items);
    }

    #endregion
}