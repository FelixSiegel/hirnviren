namespace CampusLendingSystem;

public abstract class LoanItem : ICampusItem, IComparableCustom<LoanItem>
{
    public string Name { get; }
    public uint InventoryNumber { get; }

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
        return $"Laptop {Name} in {RoomNumber} (InventoryNumber: {InventoryNumber}) is available.";
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
        return $"Book {Name} by {Author} (InventoryNumber: {InventoryNumber}) is available.";
    }
}