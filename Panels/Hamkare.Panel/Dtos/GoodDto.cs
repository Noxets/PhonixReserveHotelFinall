namespace Hamkare.Panel.Dtos;

public class GoodDto
{
    public long Id { get; set; }

    public long Count { get; set; }

    public long? BranchId { get; set; }

    public string BarCode { get; set; }

    public decimal BoxingCost { get; set; }

    public decimal Width { get; set; }

    public decimal Height { get; set; }

    public decimal Length { get; set; }

    public decimal Weight { get; set; }

    public decimal Price { get; set; }
    
    public decimal Discount { get; set; }

    public decimal Tax { get; set; }

    public decimal PercentageDiscount => Price == 0 ? 0 : (Discount / Price) * 100;

    public bool HaveDiscount => Discount != 0;

    public decimal OffPrice => Price - Discount;

    public int BasketCount { get; set; }
}