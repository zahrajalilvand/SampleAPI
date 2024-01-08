using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace APISample.Controllers
{
	[Route("api/{files}")]
	[ApiController]
	public class FileController : Controller
	{
		FileExtensionContentTypeProvider fileExtensionContentTypeProvider;

        public FileController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
        {
			this.fileExtensionContentTypeProvider = fileExtensionContentTypeProvider;

		}

		[HttpGet("fileId")]
        public ActionResult GetFile(string file)
		{
			var pathToFile = "14020821.rar";

			if (!System.IO.File.Exists(pathToFile))
			{
				return NotFound();
			}
		
			var bytes = System.IO.File.ReadAllBytes(pathToFile);

			if (!fileExtensionContentTypeProvider.TryGetContentType(pathToFile, out var contentType))
			{
				contentType = "application/octet-stream|";
			}

			return File(bytes, contentType, Path.GetFileName(pathToFile));
		}
	}
}
