using AutoMapper;
using Concert.API.CustomActionFilters;
using Concert.API.Data;
using Concert.API.Models.Domain;
using Concert.API.Models.DTO;
using Concert.API.Repositories;
using Concert.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Concert.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        // UPLOAD Image
        // POST: /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] UploadImageRequestDto uploadImageRequestDto)
        {
            ValidateFileUpload(uploadImageRequestDto);

            if (ModelState.IsValid)
            {
                // User repository to upload image
                /// @todo
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Validates file extension and size
        /// </summary>
        /// <param name="uploadImageRequestDto"></param>
        private void ValidateFileUpload(UploadImageRequestDto uploadImageRequestDto)
        {
            if (!SD.IMAGE_ALLOWED_EXTENSIONS.Contains(Path.GetExtension(uploadImageRequestDto.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            if(uploadImageRequestDto.File.Length > SD.IMAGE_MAX_LENGTH_BYTES)
            {
                ModelState.AddModelError("file", "File size greater than 10 MB");
            }
        }
    }
}
