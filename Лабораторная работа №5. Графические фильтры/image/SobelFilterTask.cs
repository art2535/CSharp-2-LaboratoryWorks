using System;

namespace Recognizer;
internal static class SobelFilterTask
{
    public static double[,] SobelFilter(double[,] g, double[,] sx)
    {
        int width = g.GetLength(0);
        int height = g.GetLength(1);
        double[,] result = new double[width, height];
        for (int x = 1; x < width - 1; x++)
        {
            for (int y = 1; y < height - 1; y++)
            {
                double[,] sy = new double[,]
                {
                        { -1, -2, -1 },
                        { 0, 0, 0 },
                        { 1, 2, 1 }
                };
                double gx = CalculateConvolution(g, sx, x, y);
                double gy = CalculateConvolution(g, sy, x, y);
                result[x, y] = Math.Sqrt(gx * gx + gy * gy);
            }
        }
        return result;
    }
    private static double CalculateConvolution(double[,] original, double[,] filter, int x, int y)
    {
        double sum = 0;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                sum += original[x + i, y + j] * filter[i + 1, j + 1];
            }
        }
        return sum;
    }
}