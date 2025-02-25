namespace PetFamily.Application.Dtos;
public class UploadFileDto
{
    public Stream Content { get; init; } = null!;
    public string FileName { get; init; } = null!;

    public UploadFileDto(Stream content, string fileName)
    {
        Content = content;
        FileName = fileName;
    }
}