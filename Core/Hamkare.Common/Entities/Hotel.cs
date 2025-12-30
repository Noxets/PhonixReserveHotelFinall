using Hamkare.Common.Entities.Generics;

namespace Hamkare.Common.Entities;

public class Hotel : MainEntity
{
    public List<string> Images { get; set; }
    
    public decimal Rate { get; set; }
    
    public string Address { get; set; }
    
    public int Star { get; set; }

    public virtual ICollection<HotelRoom> HotelRooms { get; set; }
}