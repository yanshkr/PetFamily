using PetFamily.SharedKernel.ErrorManagement;

namespace PetFamily.Framework.Response;

public record Envelope
{
    public object? Result { get; }

    public List<Error>? Errors { get; }

    public DateTime TimeGenerated { get; }

    private Envelope(object? result, IEnumerable<Error>? errors)
    {
        Result = result;
        Errors = errors?.ToList();
        TimeGenerated = DateTime.UtcNow;
    }

    public static Envelope Ok(object? result = null) => new(result, null);
    public static Envelope Error(IEnumerable<Error> errors) => new(null, errors);
}