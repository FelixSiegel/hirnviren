namespace CampusLendingSystem;

public abstract class LoanItem : ICampusItem, IComparableCustom<LoanItem>, IBorrowable
{
    public string Name { get; }
    public uint InventoryNumber { get; }

    public bool IsAvailable { get; set; } = true;

    protected LoanItem(string name, uint inventoryNumber)
    {
        Name = name;
        InventoryNumber = inventoryNumber;
    }

    public abstract string GetStatusReport();

    public int CompareWith(LoanItem otherItem)
    {
        return InventoryNumber.CompareTo(otherItem.InventoryNumber);
    }

    public bool IsGreaterThan(LoanItem otherItem)
    {
        return CompareWith(otherItem) > 0;
    }

    public bool IsLessThan(LoanItem otherItem)
    {
        return CompareWith(otherItem) < 0;
    }

    public void Borrow()
    {
        if (!IsAvailable)
        {
            throw new InvalidOperationException($"{Name} (InventoryNumber: {InventoryNumber}) is already borrowed.");
        }
        IsAvailable = false;
    }

    public void Return()
    {
        if (IsAvailable)
        {
            throw new InvalidOperationException($"{Name} (InventoryNumber: {InventoryNumber}) is not currently borrowed.");
        }
        IsAvailable = true;
    }
}

public class Laptop : LoanItem
{
    public string RoomNumber { get; }

    public Laptop(string name, uint inventoryNumber, string roomNumber) : base(name, inventoryNumber)
    {
        RoomNumber = roomNumber;
    }

    public override string GetStatusReport()
    {
        return $"Laptop {Name} in {RoomNumber} (InventoryNumber: {InventoryNumber}) is {(IsAvailable ? "available" : "not available")}";
    }
}

public class Book : LoanItem
{
    public string Author { get; }

    public Book(string name, uint inventoryNumber, string author) : base(name, inventoryNumber)
    {
        Author = author;
    }

    public override string GetStatusReport()
    {
        return $"Book {Name} by {Author} (InventoryNumber: {InventoryNumber}) is {(IsAvailable ? "available" : "not available")}";
    }
}
