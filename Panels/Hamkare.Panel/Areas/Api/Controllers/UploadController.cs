using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hamkare.Panel.Areas.Api.Controllers;

[ApiController]
[Authorize]
public class UploadController(IWebHostEnvironment webHostEnvironment) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Upload a file");

        var uploadFolderPath = Path.Combine(webHostEnvironment.WebRootPath, "upload", "FreeZone");

        if (!Directory.Exists(uploadFolderPath))
            Directory.CreateDirectory(uploadFolderPath);
        var uniqueFileName =$"{Path.GetFileNameWithoutExtension(file.FileName)} - {Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        
        var filePath = Path.Combine(uploadFolderPath, uniqueFileName);

        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var fileUrl = $"/upload/FreeZone/{uniqueFileName}";

        return Ok(new {location = fileUrl});
    }
}