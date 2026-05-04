using System;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        CelestialBody body = args.Length > 0 ? ReadInput(args) : ReadInput();
        Console.WriteLine($"Mission started with: {body.Name}");
        Console.WriteLine(body);
    }

    // Overload 1: command-line args
    static CelestialBody ReadInput(string[] args)
    {
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <Name> <CatalogNr> <Type> [SpectralClass] [Magnitude] [OrbitalPeriod] [ParentId]");
            Environment.Exit(1);
        }

        try
        {
            string name = args[0];
            uint catalogNr = Parse<uint>(args[1], uint.TryParse);
            BodyType type = ParseEnum<BodyType>(args[2]);
            SpectralClass? spectralClass = args.Length > 3 ? ParseEnum<SpectralClass>(args[3]) : null;
            float? magnitude = args.Length > 4 ? ParseFloat(args[4]) : null;
            float? period = args.Length > 5 ? ParseFloat(args[5]) : null;
            uint? parentId = args.Length > 6 ? Parse<uint>(args[6], uint.TryParse) : null;

            return new CelestialBody(name, catalogNr, type, spectralClass, magnitude, period, parentId);
        }
        catch (ArgumentException ex)
        {
            Console.Error.WriteLine($"Input error: {ex.Message}");
            Environment.Exit(1);
            return null!;
        }
    }

    // Overload 2: interactive console input
    static CelestialBody ReadInput()
    {
        Console.WriteLine("=== Celestial Body Input ===");

        string name = ReadString("Name");
        uint catalogNr = ReadValue<uint>("Catalog number (5 digits)", uint.TryParse);
        BodyType type = ReadEnum<BodyType>("Type (Star, Planet, Moon)");

        SpectralClass? spectralClass = type == BodyType.Star ? ReadEnum<SpectralClass>("Spectral class (O,B,A,F,G,K,M)") : null;
        float? magnitude = ReadYesNo("Add apparent magnitude?") ? ReadFloat("Apparent magnitude", min: 0f) : null;
        float? period = null;
        uint? parentId = null;

        if (type == BodyType.Planet || type == BodyType.Moon)
        {
            period = ReadFloat("Orbital period (Earth years)", min: 0f);
            parentId = (uint)ReadFloat("Parent body catalog number (5 digits)", min: 10000f);
        }

        return new CelestialBody(name, catalogNr, type, spectralClass, magnitude, period, parentId);
    }

    // --- parse helpers ---

    delegate bool TryParseFunc<T>(string s, out T result);

    static T Parse<T>(string s, TryParseFunc<T> tryParse)
    {
        if (!tryParse(s, out T result))
            throw new ArgumentException($"Cannot parse '{s}' as {typeof(T).Name}");
        return result;
    }

    static float ParseFloat(string s)
    {
        if (!float.TryParse(s.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out float v))
            throw new ArgumentException($"Cannot parse '{s}' as float");
        return v;
    }

    static T ParseEnum<T>(string s) where T : struct, Enum
    {
        if (!Enum.TryParse<T>(s, ignoreCase: true, out T result))
            throw new ArgumentException($"'{s}' is not a valid {typeof(T).Name}. Allowed: {string.Join(", ", Enum.GetNames<T>())}");
        return result;
    }

    // --- interactive read helpers ---

    static string ReadString(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt}: ");
            string? v = Console.ReadLine()?.Trim();
            if (!string.IsNullOrWhiteSpace(v)) return v;
            Console.WriteLine("  Cannot be empty.");
        }
    }

    static T ReadValue<T>(string prompt, TryParseFunc<T> tryParse)
    {
        while (true)
        {
            Console.Write($"{prompt}: ");
            if (tryParse(Console.ReadLine()?.Trim() ?? "", out T result)) return result;
            Console.WriteLine("  Invalid input.");
        }
    }

    static float ReadFloat(string prompt, float min = float.MinValue)
    {
        while (true)
        {
            Console.Write($"{prompt}: ");
            string raw = Console.ReadLine()?.Trim().Replace(',', '.') ?? "";
            if (float.TryParse(raw, NumberStyles.Float, CultureInfo.InvariantCulture, out float v) && v >= min)
                return v;
            Console.WriteLine($"  Must be a number >= {min}.");
        }
    }

    static T ReadEnum<T>(string prompt) where T : struct, Enum
    {
        while (true)
        {
            Console.Write($"{prompt}: ");
            if (Enum.TryParse<T>(Console.ReadLine()?.Trim(), ignoreCase: true, out T result)) return result;
            Console.WriteLine($"  Allowed: {string.Join(", ", Enum.GetNames<T>())}");
        }
    }

    static bool ReadYesNo(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt} (y/n): ");
            string? v = Console.ReadLine()?.Trim().ToLower();
            if (v == "y") return true;
            if (v == "n") return false;
            Console.WriteLine("  Enter 'y' or 'n'.");
        }
    }
}

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
}

public enum BodyType { Star, Planet, Moon }
public enum SpectralClass { O, B, A, F, G, K, M }
