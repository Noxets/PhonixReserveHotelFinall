using Hamkare.Common.Entities.Generics;

namespace Hamkare.Common.Entities;

public class HotelRoom : RootEntity
{
    public int Bed { get; set; }
    
    public int RoomNumber { get; set; }

    public decimal Price { get; set; }
    
    public decimal Tax { get; set; }
    
    public decimal Discount { get; set; }
    
    public List<string> Images { get; set; }

    public long HotelId { get; set; }

    public virtual Hotel Hotel { get; set; }
}