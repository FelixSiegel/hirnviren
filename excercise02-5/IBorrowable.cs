using CampusLendingSystem;

public interface IBorrowable
{
    bool IsAvailable { get; }
    void Borrow();
    void Return();
}
