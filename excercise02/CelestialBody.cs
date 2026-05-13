namespace RaumfahrtMission;

public enum SpectralClass { O, B, A, F, G, K, M }

public abstract class CelestialBody
{
    public string Name { get; set; }
    public uint CatalogNumber { get; set; }

    protected CelestialBody(string name, uint catalogNumber)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.");
        if (catalogNumber < 10000 || catalogNumber > 99999)
            throw new ArgumentException("CatalogNumber must be a 5-digit value (10000-99999).");

        Name = name.Trim();
        CatalogNumber = catalogNumber;
    }

    public override string ToString() => $"Name: {Name}, CatalogNumber: {CatalogNumber}";

    public override bool Equals(object? obj)
    {
        if (obj is not CelestialBody other)
            return false;

        return string.Equals(Name, other.Name, StringComparison.Ordinal);
    }

    public override int GetHashCode() => Name.GetHashCode(StringComparison.Ordinal);
}

public class Star : CelestialBody
{
    public SpectralClass SpectralClass { get; set; }
    public float ApparentMagnitude { get; set; }

    public Star(string name, uint catalogNumber, SpectralClass spectralClass, float apparentMagnitude)
        : base(name, catalogNumber)
    {
        SpectralClass = spectralClass;
        ApparentMagnitude = apparentMagnitude;
    }

    public override string ToString()
        => $"Star -> {base.ToString()}, SpectralClass: {SpectralClass}, ApparentMagnitude: {ApparentMagnitude}";
}

public class Planet : CelestialBody
{
    public float OrbitalPeriod { get; set; }
    public uint CatalogNumberReference { get; set; }

    public Planet(string name, uint catalogNumber, float orbitalPeriod, uint catalogNumberReference)
        : base(name, catalogNumber)
    {
        if (orbitalPeriod <= 0)
            throw new ArgumentException("OrbitalPeriod must be positive.");
        if (catalogNumberReference < 10000 || catalogNumberReference > 99999)
            throw new ArgumentException("CatalogNumberReference must be a 5-digit value (10000-99999).");

        OrbitalPeriod = orbitalPeriod;
        CatalogNumberReference = catalogNumberReference;
    }

    public override string ToString()
        => $"Planet -> {base.ToString()}, OrbitalPeriod: {OrbitalPeriod} years, CatalogNumberReference: {CatalogNumberReference}";
}

public class Moon : Planet
{
    public Moon(string name, uint catalogNumber, float orbitalPeriod, uint catalogNumberReference)
        : base(name, catalogNumber, orbitalPeriod, catalogNumberReference)
    {
    }

    public override string ToString() => $"Moon -> {base.ToString()}";
}
