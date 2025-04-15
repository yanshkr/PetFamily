namespace PetFamily.SharedKernel.Entities;
public abstract class SoftDeletableEntity<T> : BaseEntity<T> where T : IComparable<T>
{
    public bool IsDeleted { get; private set; }
    public DateTime DeletedAt { get; private set; }

    public void Delete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
    }

    public void Restore()
    {
        IsDeleted = false;
        DeletedAt = DateTime.MinValue;
    }
}
