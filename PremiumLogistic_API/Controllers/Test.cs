using Org.BouncyCastle.Crypto.Agreement.Srp;

namespace PremiumLogistic_API.Controllers;

[ApiController]
[Route("api/v{v:apiVersion}/{culture:culture}/[controller]")]
[ApiVersion("1.0")]
public class Test : ControllerBase
{
    [HttpPost("uploadDoc")]
    public async Task<IActionResult> Upload([FromForm] FileModel fileModel)
    {
        try
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Documents", fileModel.File.FileName);
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                fileModel.File.CopyTo(stream);
            }
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}

public class FileModel
{
    public IFormFile File { get; set; }
}
