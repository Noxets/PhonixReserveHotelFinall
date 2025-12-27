using Hamkare.Common.Entities.Generics;
using Hamkare.Resource;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hamkare.Utility.Attributes.DateTime;

namespace Hamkare.Common.Entities.Orders;

[Display(Name = nameof(EntitiesResources.OrderItem), ResourceType = typeof(EntitiesResources))]
public class OrderItem : RootEntity
{
    public decimal Price { get; set; }

    public decimal Tax { get; set; }

    public decimal Discount { get; set; }

    public long HotelRoomId { get; set; }

    public DateTime? DateTime { get; set; }

    public long OrderId { get; set; }

    [Column(TypeName = "datetime")]
    [Display(Name = nameof(GlobalResources.StartDate), ResourceType = typeof(GlobalResources))]
    [DateNotLessThen]
    public DateTime? StartDate { get; set; }

    [Display(Name = nameof(GlobalResources.Time), ResourceType = typeof(GlobalResources))]
    public long? Time { get; set; }

    [ForeignKey("OrderId")] public virtual Order Order { get; set; }

    public decimal PercentageDiscount => Price == 0 ? 0 : Math.Round((Discount / Price) * 100, 1);

    public bool HaveDiscount => Discount != 0;

    public decimal OffPrice => Price - Discount;

    public decimal TotalPrice => Price;

    public decimal TotalDiscount => Discount;

    public decimal TotalTax => Tax;

    public decimal TotalOffPrice => OffPrice;

    public decimal TotalPayable => TotalPrice + TotalTax - TotalDiscount;

    public virtual HotelRoom HotelRoom { get; set; }

    public override bool Validate(out List<string> errors)
    {
        base.Validate(out errors);

        if (TotalPayable < 0)
            errors.Add(ErrorResources.DiscountCannotTotalAmount);

        return errors.Any();
    }
}