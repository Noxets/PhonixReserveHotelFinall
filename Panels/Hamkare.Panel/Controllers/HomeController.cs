using Microsoft.AspNetCore.Mvc;

namespace Hamkare.Panel.Controllers;

public class HomeController : Controller
{
    public IActionResult DarkMode(bool isDarkMode, string returnUrl = "/")
    {
        var cookieValue = isDarkMode ? "dark" : "light";
        HttpContext.Response.Cookies.Append("DarkMode", cookieValue);

        return LocalRedirect(returnUrl);
    }
}