namespace PetFamily.SharedKernel.ErrorManagement;
public static class Errors
{
    public static class General
    {
        public static Error ValueIsInvalid(string name) => Error.Validation("value.is.invalid", $"'{name}' is invalid");
        public static Error ValueIsInvalid(string name, string reason) => Error.Validation("value.is.invalid", $"'{name}' is invalid, reason: {reason}");
        public static Error ValueIsInvalid(string name, int min, int max) => Error.Validation("value.is.invalid", $"'{name}' should be between {min} and {max}");

        public static Error NotFound(Guid id) => Error.NotFound("record.not.found", $"Record not found for id '{id}'");
        public static Error AlreadyExists(Guid id) => Error.Conflict("record.already.exists", $"Record already exists for id '{id}'");
        public static Error RelatedDataExists(Guid id) => Error.Conflict("related.data.exists", $"Related data exists for id '{id}'");
        public static Error UploadFailure(string message) => Error.UnExpected("upload.failure", message);
        public static Error ValueIsRequired(string name) => Error.Validation("value.is.required", $"'{name}' is required");
        public static Error UnExpected(string message) => Error.UnExpected("unexpected.error", message);
    }
}