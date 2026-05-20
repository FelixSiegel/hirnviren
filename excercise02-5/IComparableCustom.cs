namespace CampusLendingSystem;

public interface IComparableCustom<T>
{
    int CompareWith(T other);
    bool IsGreaterThan(T other);
    bool IsLessThan(T other);
}
