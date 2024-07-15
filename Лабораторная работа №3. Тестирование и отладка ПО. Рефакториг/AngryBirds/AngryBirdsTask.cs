using System;

namespace AngryBirds;

public static class AngryBirdsTask
{
    public static double FindSightAngle(double v, double distance)
    {
        const double g = 9.8;
        var alpha = 0.0;
        alpha = Math.Asin((distance * g) / Math.Pow(v, 2)) / 2;
        return alpha;
    }
}