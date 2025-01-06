using CSharpFunctionalExtensions;

namespace PetFamily.Domain.ValueObjects;
public record SocialMedia
{
    private SocialMedia(
        string name,
        string url
        )
    {
        Name = name;
        Url = url;
    }

    public string Name { get; }
    public string Url { get; }

    public static Result<SocialMedia, string> Create(
        string name,
        string url
        )
    {
        if (string.IsNullOrWhiteSpace(name))
            return "Name should not be empty";

        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("Url should not be empty");

        return new SocialMedia(name, url);
    }
}
