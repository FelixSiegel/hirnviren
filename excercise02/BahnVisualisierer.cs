using System;
using RaumfahrtMission;

namespace RaumfahrtMission;

public class BahnVisualisierer : OrbitBasic
{
    protected override void DrawEllipseOnCanvas(int width, int height, double a, double e, char[,] canvas, double scaleX, double scaleY)
    {
        double centerX = width / 2.0;
        double centerY = height / 2.0;

        // Place the focal point (sun) at the center horizontally
        double focusScreenX = centerX;

        // Use polar form r(θ) = a*(1-e^2)/(1+e*cos(θ)) relative to focal point
        double denomConst = a * (1 - e * e);

        for (double theta = 0; theta < Math.PI * 2; theta += 0.01)
        {
            double r = denomConst / (1.0 + e * Math.Cos(theta));
            double x = r * Math.Cos(theta); // relative to focal point
            double y = r * Math.Sin(theta);

            int sx = (int)Math.Round(focusScreenX + x * scaleX);
            int sy = (int)Math.Round(centerY - y * scaleY); // invert y for screen coords

            if (sx >= 0 && sx < width && sy >= 0 && sy < height)
            {
                canvas[sy, sx] = DrawingGlyph;
            }
        }
    }

    protected override void DrawFocalPoint(int width, int height, double a, double e, char[,] canvas, double scaleX)
    {
        double centerX = width / 2.0;
        double centerY = height / 2.0;

        // focal point (sun) is at the origin of polar coordinates used above
        int fx = (int)Math.Round(centerX);
        int fy = (int)Math.Round(centerY);

        if (fx >= 0 && fx < width && fy >= 0 && fy < height)
            canvas[fy, fx] = FocalPointSymbol;
    }
}