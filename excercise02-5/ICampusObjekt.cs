namespace CampusLendingSystem;

public interface ICampusItem
{
    string Name { get; }
    uint InventoryNumber { get; }
    string GetStatusReport();
}