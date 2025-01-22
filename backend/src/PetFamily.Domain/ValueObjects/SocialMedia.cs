using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace PetFamily.Domain.ValueObjects;
public record SocialMedia
{
    private const string URL_REGEX = @"^https:\/\/([\w\-]+\.)+[\w\-]+(\/[\w\-._~:\/?#[\]@!$&'()*+,;=%]*)?$";
    public const int MAX_NAME_LENGTH = 100;

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
        if (string.IsNullOrWhiteSpace(name) || name.Length <= MAX_NAME_LENGTH)
            return "Name should not be empty";

        if (!Regex.IsMatch(url, URL_REGEX))
            return "Url is not valid";

        return new SocialMedia(name, url);
    }
}
