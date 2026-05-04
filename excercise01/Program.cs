using System;

class Program
{
    static void Main(string[] args)
    {
        CelestialBody body = readStdIn(args);
        CelestialBody proxima = new CelestialBody("Proxima Centauri b", 12345, BodyType.Planet)
        {
            OrbitalPeriod = 0.0315f,
            ParentBodyId = 67890
        };

        Console.WriteLine($"Mission started with: {body.Name}");
        Console.WriteLine(body);
    }

    static CelestialBody readStdIn(string[] args)
    {
        CelestialBody body = new CelestialBody(
                args[0],
                uint.Parse(args[1]),
                Enum.Parse<BodyType>(args[2]),
                Enum.Parse<SpectralClass>(args[3]),
                float.Parse(args[4]),
                float.Parse(args[5]),
                uint.Parse(args[6])
                );
        return body;
    }
}

public class CelestialBody
{
    // Required properties
    public string Name { get; set => field = value.Trim(); }
    public uint CatalogNumber { get; set; }
    public BodyType Type { get; set; }

    // Optional properties (Nullable)
    public SpectralClass? StarClass { get; set; }
    public float? ApparentMagnitude { get; set; }  // "Scheinbare Helligkeit"
    public float? OrbitalPeriod { get; set; }      // "Umlaufzeit"
    public uint? ParentBodyId { get; set; }        // "Zentralkörper-Katalog-Nummer"

    public CelestialBody(string name, uint catalogNumber, BodyType typ, SpectralClass? starClass = null, float? magnitude = null, float? period = null, uint? id = null)
    {
        // check if user is stupid
        if (!(10000 <= catalogNumber && catalogNumber < 100000)) { throw new Exception("Winkler?? 5 stellig ist doch im Normalfall 10-tausender Bereich!?"); }
        if (magnitude < 0) { throw new Exception("Was bitte ist eine negative Helligkeit??!"); }
        if (typ == BodyType.Planet || typ == BodyType.Moon)
        {
            if (period < 0) { throw new Exception($"Der {typ} kann sich nicht rückwärts drehen junge!"); }
            if (!(10000 <= id && id < 100000)) { throw new Exception("Winkler?? 5 stellig ist doch im Normalfall 10-tausender Bereich!?"); }
        }
        Name = name;
        CatalogNumber = catalogNumber;
        Type = typ;
        StarClass = starClass;
        ApparentMagnitude = magnitude;
        OrbitalPeriod = period;
        ParentBodyId = id;
    }

    public override string ToString()
    {
        string result = $"Himmelskörper: {Name}, Katalog-Nummer: {CatalogNumber}, Typ: {Type}, ";
        if (Type == BodyType.Star)
        {
            result += $"Spektralklasse: {StarClass}, Scheinbare Helligkeit: {ApparentMagnitude}";
        }
        else
        {
            result += $"Umlaufzeit: {OrbitalPeriod} Erdjahre, Zentralkörper-Katalog-Nummer: {ParentBodyId}";
        }
        return result;
    }
}

public enum BodyType { Star, Planet, Moon }

public enum SpectralClass { O, B, A, F, G, K, M }
