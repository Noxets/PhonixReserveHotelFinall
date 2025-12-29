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
        return View();
    }
    
    [HttpGet("/Hotel/[id]")]
    public async Task<IActionResult> Hotel(long id, CancellationToken cancellationToken)
    {
        return View();
    }
}