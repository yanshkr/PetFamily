namespace PetFamily.Domain.Shared;
public static class Errors
{
    public static class General
    {
        public static Error ValueIsInvalid(string name) => Error.Validation("value.is.invalid", $"'{name}' is invalid");
        public static Error ValueIsInvalid(string name, string reason) => Error.Validation("value.is.invalid", $"'{name}' is invalid, reason: {reason}");
        public static Error ValueIsInvalid(string name, int min, int max) => Error.Validation("value.is.invalid", $"'{name}' should be between {min} and {max}");

        public static Error NotFound(Guid id) => Error.NotFound("record.not.found", $"Record not found for id '{id}'");
        public static Error ValueIsRequired(string name) => Error.Validation("value.is.required", $"'{name}' is required");
        public static Error UnExpected(string message) => Error.UnExpected("unexpected.error", message);
    }
}