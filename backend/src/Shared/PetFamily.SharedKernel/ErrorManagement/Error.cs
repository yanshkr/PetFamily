using System.Text.Json.Serialization;

namespace PetFamily.SharedKernel.ErrorManagement;
public record Error
{
    private const string SEPARATOR = "||";

    private Error(string code, string message, ErrorType type)
    {
        Code = code;
        Message = message;
        Type = type;
    }

    public string Code { get; }
    public string Message { get; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ErrorType Type { get; }

    public static Error Validation(string code, string message) => new(code, message, ErrorType.Validation);
    public static Error NotFound(string code, string message) => new(code, message, ErrorType.NotFound);
    public static Error Failure(string code, string message) => new(code, message, ErrorType.Failure);
    public static Error Conflict(string code, string message) => new(code, message, ErrorType.Conflict);
    public static Error UnExpected(string code, string message) => new(code, message, ErrorType.UnExpected);

    public static string Serialize(Error error) => string.Join(SEPARATOR, error.Code, error.Message, error.Type);
    public static Error Deserialize(string serialized)
    {
        var parts = serialized.Split(SEPARATOR);

        if (parts.Length != 3)
            throw new ArgumentException("Serialized error is invalid", nameof(serialized));

        if (!Enum.TryParse<ErrorType>(parts[2], out var enumValue))
            throw new ArgumentException("Serialized error type is invalid", nameof(serialized));

        return new Error(parts[0], parts[1], enumValue);
    }

    public ErrorList ToErrorList() => new([this]);
}
public enum ErrorType
{
    Validation,
    NotFound,
    Failure,
    Conflict,
    UnExpected
}