using Hamkare.Common.Entities;
using Hamkare.Demo.ViewModels;
using Hamkare.Infrastructure;
using Hamkare.Service.Services.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Hamkare.Demo.Controller;

public class HomeController(RootService<Hotel,ApplicationDbContext> HotelService, RootService<HotelRoom,ApplicationDbContext> HotelRoomService) : Microsoft.AspNetCore.Mvc.Controller
{
    [HttpGet("/")]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var hotels = await HotelService.GetAllAsync(cancellationToken);

        return View(hotels);
    }
    
    [HttpGet("/Hotel/[id]")]
    public async Task<IActionResult> Hotel(long id, CancellationToken cancellationToken)
    {
        var hotels = await HotelService.GetActiveAsync(x => x.Id ==  id,cancellationToken);
        return View(new HotelBookingViewModel
        {
            Hotel = hotels,
            RoomId = null,
            CheckInDate = DateTime.Today.AddDays(1),
            CheckOutDate = DateTime.Today.AddDays(2)
        });
    }
    
    [HttpGet("/Hotels")]
    public async Task<IActionResult> Hotels(CancellationToken cancellationToken)
    {
        var hotels = await HotelService.GetAllAsync(cancellationToken);
        return View(hotels);
    }
    
    [HttpPost]
    public IActionResult Book(HotelBookingViewModel model)
    {
        if (!ModelState.IsValid)
            return RedirectToAction("Details", new { id = model.RoomId });

        // اینجا منطق رزرو
        // model.RoomId
        // model.CheckInDate
        // model.CheckOutDate

        return RedirectToAction("Confirm");
    }
}
