 using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO.Request;
using NZWalks.API.Repositories.Abstractions;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        [Route("Upload")]
        [ValidateModelAttribute]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imageUploadRequestDto)
        {
            ValidateFileUpload(imageUploadRequestDto);
            if (ModelState.IsValid)
            {
                var imageDomailModel = new Image
                {
                    File = imageUploadRequestDto.File,
                    FileExtention = Path.GetExtension(imageUploadRequestDto.File.FileName),
                    FileSizeBytes = imageUploadRequestDto.File.Length,
                    FileName = imageUploadRequestDto.File.FileName,
                    FileDescription = imageUploadRequestDto.FileDescription,
                };
                await imageRepository.Upload(imageDomailModel);
                return Ok(imageDomailModel);
            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDto imageUploadRequestDto)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtensions.Contains(Path.GetExtension(imageUploadRequestDto.File.FileName)))
            {
                ModelState.AddModelError("File","UnSupported File");
            }
            if(imageUploadRequestDto.File.Length > 5242880)
            {
                ModelState.AddModelError("File", "File size more then 5 mb");
            }
        }
    }
}
