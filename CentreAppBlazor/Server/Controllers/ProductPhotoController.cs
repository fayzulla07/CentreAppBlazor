using CentreAppBlazor.Server.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static CentreAppBlazor.Server.Extensions.ImageHelper;

namespace CentreAppBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductPhotoController : ControllerBase
    {
        private readonly DapperContext dapperContext;

        public ProductPhotoController(DapperContext dapperContext)
        {
            this.dapperContext = dapperContext;
        }
        [HttpGet("{id}/{imageSize}")]
        public async Task<IActionResult> Get(int id, ImageSize imageSize)
        {
            var photoArray = await dapperContext.QueryAsync<(byte[] Photo, string Extension)>("select Photo, Extension from Products where id = @id", new { id = id });
            if (photoArray == null) return NotFound();
            switch (imageSize)
            {
                case ImageSize.Normal:
                    return File(photoArray.FirstOrDefault().Photo, "application/octet-stream", $"ProductImage_{id}.png");
                case ImageSize.Small:
                    return File(GetReducedImage(100, 80, photoArray.FirstOrDefault().Photo), "application/octet-stream", $"ProductImage_{id}{photoArray.FirstOrDefault().Extension}");
            }
            return BadRequest();
        }
        [HttpPost("{id}"), DisableRequestSizeLimit]
        public async Task<IActionResult> Update(int id, IFormFile file)
        {
            var data = new { ex = Path.GetExtension(file.FileName), Photo = ReadFully(file.OpenReadStream()) };
            await dapperContext.ExecuteAsync("Update Products set Photo = @Photo, Extension = @ex", data);
            return Ok();
        }

      

    }
}
