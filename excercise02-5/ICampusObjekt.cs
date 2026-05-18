namespace CampusLeihsystem;

public interface ICampusObjekt
{
    string Name { get; }
    uint InventarNummer { get; }
    string GetStatusBericht();
}