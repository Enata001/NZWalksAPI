using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories.Interface;

namespace NZWalks.API.Repositories.Class;

public class ImageRepository : IImageRepository
{
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly AppDbContext _context;

    public ImageRepository(IWebHostEnvironment hostEnvironment, IHttpContextAccessor contextAccessor,
        AppDbContext context)
    {
        _hostEnvironment = hostEnvironment;
        _contextAccessor = contextAccessor;
        _context = context;
    }

    public async Task<Image> Upload(Image image)
    {
        var localFilePath =
            Path.Combine(_hostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");

        await using var stream = new FileStream(localFilePath, FileMode.Create);
        await image.File.CopyToAsync(stream);

        var urlFilePath =
            $"{_contextAccessor.HttpContext.Request.Scheme}://{_contextAccessor.HttpContext.Request.Host}{_contextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
        image.FilePath = urlFilePath;

        await _context.Images.AddAsync(image);
        await _context.SaveChangesAsync();

        return image;
    }
}