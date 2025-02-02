using Microsoft.AspNetCore.Mvc;
using PetFamily.Api.Extensions;
using PetFamily.Application.Features.Files.DeleteFile;
using PetFamily.Application.Features.Files.DeleteFile.Contracts;
using PetFamily.Application.Features.Files.GetFileUri;
using PetFamily.Application.Features.Files.GetFileUri.Contracts;
using PetFamily.Application.Features.Files.UploadFile;
using PetFamily.Application.Features.Files.UploadFile.Contracts;

namespace PetFamily.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class FilesController : Controller
{
    [HttpPost]
    public async Task<IActionResult> Upload(
        IFormFile formFile,
        [FromServices] UploadFileHandler uploadFileHandler,
        CancellationToken cancellationToken)
    {
        var command = new UploadFileCommand(formFile.OpenReadStream(), formFile.FileName);

        var result = await uploadFileHandler.Handle(command, cancellationToken);

        return result.ToResponse();
    }
    [HttpGet]
    public async Task<IActionResult> GetFileUri(
        [FromQuery] string fileName,
        [FromServices] GetFileUriHandler getFileUriHandler,
        CancellationToken cancellationToken)
    {
        var command = new GetFileUriCommand(fileName);

        var result = await getFileUriHandler.Handle(command, cancellationToken);

        return result.ToResponse();
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteFile(
        [FromQuery] string fileName,
        [FromServices] DeleteFileHandler deleteFileHandler,
        CancellationToken cancellationToken)
    {
        var command = new DeleteFileCommand(fileName);

        var result = await deleteFileHandler.Handle(command, cancellationToken);

        return result.ToResponse();
    }
}
