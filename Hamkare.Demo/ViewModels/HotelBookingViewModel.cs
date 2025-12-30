using Hamkare.Common.Entities;

namespace Hamkare.Demo.ViewModels;

public class HotelBookingViewModel
{
    public Hotel Hotel { get; set; }

    public int? RoomId { get; set; }

    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }
}
