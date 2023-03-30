using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace RestaurantAPI.Controllers
{
    [Route("file")]
    [Authorize]
    public class FileController : ControllerBase
    {
        [HttpGet]
        [ResponseCache(Duration = 1200, VaryByQueryKeys = new[] {"fileName"})]
        public ActionResult GetFile([FromQuery]string filename) 
        {
            var rootPath = Directory.GetCurrentDirectory();

            var filePath = $"{rootPath}\\PrivateFiles\\{filename}";

            if(!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var fileContent = System.IO.File.ReadAllBytes(filePath);

            var contentProvider = new FileExtensionContentTypeProvider();
            contentProvider.TryGetContentType(filePath, out var contentType);

            return File(fileContent, contentType, filename);
        }

        [HttpPost]
        public ActionResult Upload([FromForm]IFormFile file)
        {
            if (file is null && file.Length <= 0) 
            {
                return BadRequest();
            }
            var rootPath = Directory.GetCurrentDirectory();
            var fullPath = $"{rootPath}\\PrivateFiles\\{file.FileName}";
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return Ok();
        }
    }
}
