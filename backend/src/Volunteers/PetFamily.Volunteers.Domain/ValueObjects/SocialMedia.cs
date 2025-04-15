using CSharpFunctionalExtensions;
using PetFamily.SharedKernel.ErrorManagement;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace PetFamily.Volunteers.Domain.ValueObjects;
public record SocialMedia
{
    private const string URL_REGEX = @"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$";
    public const int MAX_NAME_LENGTH = 100;

    [JsonConstructor]
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

    public static Result<SocialMedia, Error> Create(string name, string url)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > MAX_NAME_LENGTH)
            return Errors.General.ValueIsInvalid("Name");

        if (!Regex.IsMatch(url, URL_REGEX))
            return Errors.General.ValueIsInvalid("Url");

        return new SocialMedia(name, url);
    }
}
