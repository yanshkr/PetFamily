using CSharpFunctionalExtensions;
using PetFamily.SharedKernel.ErrorManagement;

namespace PetFamily.Volunteers.Domain.ValueObjects;

public record PetPosition
{
    private const int MIN_VALUE = 1;

    private PetPosition(int value)
    {
        Value = value;
    }

    public int Value { get; }

    public static PetPosition Start => new(MIN_VALUE);
    public static Result<PetPosition, Error> Create(int value)
    {
        if (value < MIN_VALUE)
            return Errors.General.ValueIsInvalid("SerialNumber");

        return new PetPosition(value);
    }

    public static bool operator >(PetPosition petPosition1, PetPosition petPosition2) => petPosition1.Value > petPosition2.Value;
    public static bool operator <(PetPosition petPosition1, PetPosition petPosition2) => petPosition1.Value < petPosition2.Value;

    public static bool operator >=(PetPosition petPosition1, PetPosition petPosition2) => petPosition1.Value >= petPosition2.Value;
    public static bool operator <=(PetPosition petPosition1, PetPosition petPosition2) => petPosition1.Value <= petPosition2.Value;
}
