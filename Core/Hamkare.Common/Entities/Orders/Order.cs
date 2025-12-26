using Hamkare.Resource;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hamkare.Common.Entities.Generics;
using Hamkare.Common.Entities.Identity;
using Hamkare.Resource.Orders;
using Hamkare.Utility.Attributes;
using Hamkare.Utility.Attributes.DateTime;
using ZarinPal.Class;

namespace Hamkare.Common.Entities.Orders;

[Module]
[Display(Name = nameof(EntitiesResources.Order), ResourceType = typeof(EntitiesResources))]
public class Order : RootEntity
{
    public long UserId { get; set; }

    [Column(TypeName = "datetime")]
    [Display(Name = nameof(OrdersResources.OrderDate), ResourceType = typeof(OrdersResources))]
    [DateNotLessThen(nameof(PreOrderDate))]
    public DateTime OrderDate { get; set; } = DateTime.Now;

    [Display(Name = nameof(OrdersResources.OrderState), ResourceType = typeof(OrdersResources))]
    public OrderState State { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    [Display(Name = nameof(GlobalResources.Discount), ResourceType = typeof(GlobalResources))]
    public decimal Discount { get; set; }

    [Column(TypeName = "datetime")]
    [DateNotGreaterThen(nameof(OrderDate))]
    [Display(Name = nameof(OrdersResources.PreOrderDate), ResourceType = typeof(OrdersResources))]
    public DateTime? PreOrderDate { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();


    [ForeignKey("UserId")]
    public virtual User User { get; set; }
    

    [NotMapped]
    [Display(Name = nameof(GlobalResources.Price), ResourceType = typeof(GlobalResources))]
    public virtual decimal Price => OrderItems.GetAllActive().Sum(x => x.TotalPrice);

    [NotMapped]
    [Display(Name = nameof(GlobalResources.Tax), ResourceType = typeof(GlobalResources))]
    public virtual decimal Tax => OrderItems.GetAllActive().Sum(x => x.TotalTax);

    [NotMapped]
    [Display(Name = nameof(OrdersResources.TotalDiscount), ResourceType = typeof(OrdersResources))]
    public virtual decimal TotalDiscount => OrderItems.GetAllActive().Sum(x => x.TotalDiscount) + Discount;

    public bool CanEdit => State is OrderState.Basket or OrderState.ApprovalOrder or OrderState.Order;
    
    [NotMapped]
    [Display(Name = nameof(OrdersResources.TotalPrice), ResourceType = typeof(OrdersResources))]
    public virtual decimal TotalPrice => Price + Tax;

    [NotMapped]
    [Display(Name = nameof(OrdersResources.TotalPayable), ResourceType = typeof(OrdersResources))]
    public virtual decimal TotalPayable => TotalPrice - TotalDiscount - User.Wallet <= 0 ? 0 : TotalPrice - TotalDiscount - User.Wallet;
    
    [NotMapped]
    [Display(Name = nameof(OrdersResources.TotalPayable), ResourceType = typeof(OrdersResources))]
    public virtual decimal TotalPayableWithoutWallet => TotalPrice - TotalDiscount;
    
    public override bool Validate(out List<string> errors)
    {
        base.Validate(out errors);

        return errors.Any();
    }
}