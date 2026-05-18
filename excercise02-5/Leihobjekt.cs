using CampusLeihsystem;

public abstract class Leihobjekt : ICampusObjekt, IVergleichbar<Leihobjekt>
{
    public string Name { get; }
    public uint InventarNummer { get; }

    protected Leihobjekt(string name, uint inventarNummer)
    {
        Name = name;
        InventarNummer = inventarNummer;
    }
    public abstract string GetStatusBericht();

    public int VergleicheMit(Leihobjekt anderesObjekt)
    {
        return InventarNummer.CompareTo(anderesObjekt.InventarNummer);
    }

    public bool IstGroesserAls(Leihobjekt anderesObjekt)
    {
        return VergleicheMit(anderesObjekt) > 0;
    }

    public bool IstKleinerAls(Leihobjekt anderesObjekt)
    {
        return VergleicheMit(anderesObjekt) < 0;
    }
}

public class Laptop : Leihobjekt
{
    public string RaumNummer { get; }

    public Laptop(string name, uint inventarNummer, string raumNummer) : base(name, inventarNummer)
    {
        RaumNummer = raumNummer;
    }

    public override string GetStatusBericht()
    {
        return $"Laptop {Name} in {RaumNummer} (InventarNummer: {InventarNummer}) ist verfügbar.";
    }
}

public class Buch : Leihobjekt
{
    public string Autor { get; }

    public Buch(string name, uint inventarNummer, string autor) : base(name, inventarNummer)
    {
        Autor = autor;
    }

    public override string GetStatusBericht()
    {
        return $"Buch {Name} von {Autor} (InventarNummer: {InventarNummer}) ist verfügbar.";
    }
}