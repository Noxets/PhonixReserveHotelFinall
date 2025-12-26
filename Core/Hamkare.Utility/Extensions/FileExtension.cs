using Hamkare.Utility.Settings.CoreSettings;
using System.Drawing;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Xabe.FFmpeg;

namespace Hamkare.Utility.Extensions;

public static class FileExtension
{
    private static List<string> ImageFormats { get; set; } = [".jpg", ".jpeg", ".png", ".gif", ".webp", ".svg"];
    public static string ImageFormatString => string.Join(", ", ImageFormats);

    private static List<string> VideoFormat { get; set; } = [".mp4", ".webm", ".ogg", ".av1"];
    public static string VideoFormatString => string.Join(", ", VideoFormat);

    public static async Task<string> AddFile(this IBrowserFile browserFile, IWebHostEnvironment webHostEnvironment,
        string folderRoot)
    {
        var uniqueFileName =
            $"{Path.GetFileNameWithoutExtension(browserFile.Name)} - {Guid.NewGuid()}{Path.GetExtension(browserFile.Name)}";

        var uploadFolderPath = Path.Combine(webHostEnvironment.WebRootPath, "upload", folderRoot);

        var filePath = Path.Combine(uploadFolderPath, uniqueFileName);

        if (!Directory.Exists(uploadFolderPath))
            Directory.CreateDirectory(uploadFolderPath);

        await using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite,
            FileShare.Inheritable, GlobalSettings.UploadLimit);

        await browserFile.OpenReadStream(browserFile.Size).CopyToAsync(fileStream);

        return uniqueFileName;
    }
    
    public static async Task<string> AddFile(this IFormFile formFile, IWebHostEnvironment webHostEnvironment,
        string folderRoot)
    {
        var uniqueFileName =
            $"{Path.GetFileNameWithoutExtension(formFile.FileName)} - {Guid.NewGuid()}{Path.GetExtension(formFile.FileName)}";

        var uploadFolderPath = Path.Combine(webHostEnvironment.WebRootPath, "upload", folderRoot);

        var filePath = Path.Combine(uploadFolderPath, uniqueFileName);

        if (!Directory.Exists(uploadFolderPath))
            Directory.CreateDirectory(uploadFolderPath);

        await using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite,
            FileShare.Inheritable, GlobalSettings.UploadLimit);

        await formFile.CopyToAsync(fileStream);

        return uniqueFileName;
    }


    public static void RemoveFile(string uniqueFileName, IWebHostEnvironment webHostEnvironment, string folderRoot)
    {
        var filePath = Path.Combine(webHostEnvironment.WebRootPath, "upload", folderRoot, uniqueFileName);

        if (File.Exists(filePath))
            File.Delete(filePath);
    }

    public static bool ValidateImage(this IBrowserFile browserFile, int width, int height)
    {
        try
        {
#pragma warning disable CA1416
            using var image = Image.FromStream(browserFile.OpenReadStream());
            return image.Width == width && image.Height == height;
#pragma warning restore CA1416
        }
        catch (Exception e)
        {
            Console.WriteLine($@"Error checking image size: {e.Message}");
            throw new Exception
            {
                HelpLink = null,
                HResult = 0,
                Source = "Validate"
            };
        }
    }

    public static async Task<bool> ValidateVideo(this IBrowserFile browserFile, int width, int height)
    {
        try
        {
            var mediaInfo = await FFmpeg.GetMediaInfo(browserFile.Name);
            var videoStream = mediaInfo.VideoStreams.FirstOrDefault();

            return videoStream != null && videoStream.Width == width && videoStream.Height == height;
        }
        catch (Exception e)
        {
            Console.WriteLine($@"Error checking video size: {e.Message}");

            throw new Exception
            {
                HelpLink = null,
                HResult = 0,
                Source = "Validate"
            };
        }
    }

    public static string GetFileNameFromUrl(this string url)
    {
        var uri = new Uri(url);
        return Path.GetFileName(uri.LocalPath);
    }
}