using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
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

    public static Result<SocialMedia, Error> Create(
        string name,
        string url
        )
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > MAX_NAME_LENGTH)
            return Errors.General.ValueIsInvalid("Name");

        if (!Regex.IsMatch(url, URL_REGEX))
            return Errors.General.ValueIsInvalid("Url");

        return new SocialMedia(name, url);
    }
}
