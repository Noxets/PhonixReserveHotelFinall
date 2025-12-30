using Hamkare.Common.Entities;
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
        return View();
    }
    
    [HttpGet("/Hotels")]
    public async Task<IActionResult> Hotels(CancellationToken cancellationToken)
    {
        var hotels = await HotelService.GetAllAsync(cancellationToken);
        return View(hotels);
    }
}