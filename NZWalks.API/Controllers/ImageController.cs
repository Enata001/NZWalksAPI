using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories.Interface;

namespace NZWalks.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImageController : ControllerBase
{
    private readonly IImageRepository _repository;

    public ImageController(IImageRepository repository)
    {
        _repository = repository;
    }
    [HttpPost]
    [Route("Upload")]
    public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imageRequest)
    {
        

            ValidateFile(imageRequest);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var imageDomain = new Image()
            {
                File = imageRequest.File,
                FileName = imageRequest.FileName,
                FileDescription = imageRequest.FileDescription ?? "",
                FileExtension = Path.GetExtension(imageRequest.File.FileName),
                FileSize = imageRequest.File.Length
            };


            var result =  await _repository.Upload(imageDomain);
        
        
            return Ok(result);
       
    }

   private void ValidateFile(ImageUploadRequestDto imageRequest)
    {
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
        if (!allowedExtensions.Contains(Path.GetExtension(imageRequest.File.FileName)))
        {
            ModelState.AddModelError("File", "Unsupported File Type");
        }

        if (imageRequest.File.Length > 10485760)
        {
            ModelState.AddModelError("File", "File size more than 10 MB. Please upload a smaller image");
        }
    }
}