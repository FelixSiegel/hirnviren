namespace RaumfahrtMission {



public enum BodyType { Star, Planet, Moon }
public enum SpectralClass { O, B, A, F, G, K, M }

public class CelestialBody
{
    // Required properties
    public string Name { get; set; }
    public uint CatalogNumber { get; set; }
    public BodyType Type { get; set; }

    // Optional (nullable) properties
    public SpectralClass? StarClass { get; set; }
    public float? ApparentMagnitude { get; set; }
    public float? OrbitalPeriod { get; set; }
    public uint? ParentBodyId { get; set; }

    // Read-only properties
    public float? OribitalPeriodInDays => OrbitalPeriod.HasValue ? OrbitalPeriod.Value * 356.25f : null;
    public string FullType => StarClass.HasValue ? $"Star ({StarClass}-Class)" : $"{Type}";

    public CelestialBody(string name, uint catalogNumber, BodyType type,
        SpectralClass? starClass = null, float? magnitude = null,
        float? period = null, uint? parentId = null)
    {
        Validate(name, catalogNumber, type, starClass, magnitude, period, parentId);

        Name = name.Trim();
        CatalogNumber = catalogNumber;
        Type = type;
        StarClass = starClass;
        ApparentMagnitude = magnitude;
        OrbitalPeriod = period;
        ParentBodyId = parentId;
    }

    static void Validate(string name, uint catalogNumber, BodyType type,
        SpectralClass? starClass, float? magnitude, float? period, uint? parentId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty.");
        if (catalogNumber < 10000 || catalogNumber > 99999)
            throw new ArgumentException($"Catalog number must be 5 digits (10000–99999), got: {catalogNumber}");
        if (magnitude.HasValue && magnitude.Value < 0f)
            throw new ArgumentException($"Apparent magnitude cannot be negative, got: {magnitude.Value}");
        if (type == BodyType.Star && starClass == null)
            throw new ArgumentException("Stars must have a spectral class.");
        if (type == BodyType.Planet || type == BodyType.Moon)
        {
            if (!period.HasValue || period.Value <= 0f)
                throw new ArgumentException($"{type} requires a positive orbital period.");
            if (!parentId.HasValue || parentId.Value < 10000 || parentId.Value > 99999)
                throw new ArgumentException($"{type} requires a valid 5-digit parent body catalog number.");
        }
    }

    public override string ToString()
    {
        string s = $"Himmelskörper: {Name}, Katalog-Nummer: {CatalogNumber}, Typ: {Type}, ";
        if (Type == BodyType.Star)
        {
            s += $"Spektralklasse: {StarClass}";
            if (ApparentMagnitude.HasValue) s += $", Scheinbare Helligkeit: {ApparentMagnitude.Value}";
        }
        else
        {
            s += $"Umlaufzeit: {OrbitalPeriod} Erdjahre, Zentralkörper-Katalog-Nummer: {ParentBodyId}";
        }
        if (OribitalPeriodInDays.HasValue)
        {
            s += $", Orbital period: {OribitalPeriodInDays.Value} days";
        }
        s += $", Full type: {FullType}";
        return s;
    }

    public static bool operator ==(CelestialBody? left, CelestialBody? right)
    {
        if (ReferenceEquals(left, right)) return true; // reference to the same object
        if (left is null || right is null) return false;
        return left.Name == right.Name;
    }

    public static bool operator !=(CelestialBody? left, CelestialBody? right)
    {
        return !(left == right); // just flip the equal method lol
    }

    // Overriding equal operator requires overriding Equals and GetHashCode method as well
    public override bool Equals(object? obj)
    {
        // The cast will either cast to CelestialBody or null
        // then we just use the defined equal operator from above (which handles null values)
        return this == obj as CelestialBody;
    }

    public override int GetHashCode() => Name.GetHashCode();
}
}
