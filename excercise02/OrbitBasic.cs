using RaumfahrtMission;

namespace RaumfahrtMission;

public abstract class OrbitBasic
{
    protected const char DrawingGlyph = '*';
    protected const char FocalPointSymbol = 'O';
    protected const char EmptyCharacter = ' ';

    public void DrawOrbitAscii(OrbitalData orbitalData, int width = 60, int height = 30)
    {
        double a = orbitalData.SemiMajorAxis; // Semi-major axis in AU
        double e = orbitalData.Eccentricity;   // Eccentricity

        char[,] canvas = CreateEmptyCanvas(width, height);

        // Dynamic scaling based on semi-major axis and eccentricity
        double maxRadius = a * (1 + e); // Maximum distance from focal point
        double scaleX = (width - 10) / (2 * maxRadius);   // 10 = margin
        double scaleY = (height - 5) / (2 * maxRadius);   // 5 = margin

        // Draw the ellipse
        DrawEllipseOnCanvas(width, height, a, e, canvas, scaleX, scaleY);

        // Draw the focal point (Sun)
        DrawFocalPoint(width, height, a, e, canvas, scaleX);

        // Output
        OutputCanvas(orbitalData, width, height, e, canvas);
    }

    private static char[,] CreateEmptyCanvas(int width, int height)
    {
        char[,] canvas = new char[height, width];
        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                canvas[y, x] = EmptyCharacter;
        return canvas;
    }

    protected abstract void DrawEllipseOnCanvas(int width, int height, double a, double e, char[,] canvas, double scaleX, double scaleY);

    protected abstract void DrawFocalPoint(int width, int height, double a, double e, char[,] canvas, double scaleX);

    private static void OutputCanvas(OrbitalData orbitalData, int width, int height, double e, char[,] canvas)
    {
        Console.WriteLine($"=== Orbit of {orbitalData.CelestialBody.Name} (Eccentricity: {e:F4}) ===");
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
                Console.Write(canvas[y, x]);
            Console.WriteLine();
        }
        Console.WriteLine(new string('=', 44));
    }
}