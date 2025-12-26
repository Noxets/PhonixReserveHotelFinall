using System.ComponentModel.DataAnnotations;
using Hamkare.Resource;

namespace Hamkare.Common.Entities.Orders;

public enum OrderState
{
    /// <summary>
    /// سبد خرید
    /// امکان ویرایش
    /// </summary>
    [Display(Name = nameof(EntitiesResources.Basket), ResourceType = typeof(EntitiesResources))]
    Basket,

    /// <summary>
    /// در انتظار تایید
    /// امکان ویرایش
    /// </summary>
    [Display(Name = nameof(EntitiesResources.ApprovalOrder), ResourceType = typeof(EntitiesResources))]
    ApprovalOrder,

    /// <summary>
    /// فاکتور
    /// امکان ویرایش
    /// </summary>
    [Display(Name = nameof(EntitiesResources.Order), ResourceType = typeof(EntitiesResources))]
    Order,

    /// <summary>
    /// در حال آماده سازی
    /// </summary>
    [Display(Name = nameof(EntitiesResources.Inprogress), ResourceType = typeof(EntitiesResources))]
    Inprogress,

    /// <summary>
    /// ارسال شده
    /// </summary>
    [Display(Name = nameof(EntitiesResources.InShipping), ResourceType = typeof(EntitiesResources))]
    InShipping,

    /// <summary>
    /// تحویل شده
    /// </summary>
    [Display(Name = nameof(EntitiesResources.Close), ResourceType = typeof(EntitiesResources))]
    Close,

    /// <summary>
    /// کنسل شده
    /// </summary>
    [Display(Name = nameof(EntitiesResources.Cancel), ResourceType = typeof(EntitiesResources))]
    Cancel
}