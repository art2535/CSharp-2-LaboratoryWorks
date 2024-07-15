using System;

namespace DistanceTask;

public static class DistanceTask
{
    // Расстояние от точки (x, y) до отрезка AB с координатами A(ax, ay), B(bx, by)
    public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
    {
        double abx = bx - ax;
        double aby = by - ay;
        double apx = x - ax;
        double apy = y - ay;
        double abLenght = Math.Sqrt(abx * abx + aby * aby);
        double abScalarProduct = abx * apx + aby * apy;
        if (abLenght < 1e-10)
            return Math.Sqrt(apx * apx + apy * apy);
        double t = Math.Max(0, Math.Min(1, abScalarProduct / (abLenght * abLenght)));
        double projectionX = ax + abx * t;
        double projectionY = ay + aby * t;
        return Math.Sqrt((x - projectionX) * (x - projectionX) + (y - projectionY) * (y - projectionY));
    }
}