using CSharpFunctionalExtensions;

namespace PetFamily.SharedKernel.Entities;
public abstract class BaseEntity<T> : Entity<T> where T : IComparable<T>
{
    protected BaseEntity()
    {
        UpdatedAt = DateTime.UtcNow;
        CreatedAt = DateTime.UtcNow;
    }

    public DateTime UpdatedAt { get; private set; }
    public DateTime CreatedAt { get; private set; }
}
