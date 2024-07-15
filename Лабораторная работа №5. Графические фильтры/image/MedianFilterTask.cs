using System;

namespace Recognizer;

internal static class MedianFilterTask
{
	/* 
	 * Для борьбы с пиксельным шумом, подобным тому, что на изображении,
	 * обычно применяют медианный фильтр, в котором цвет каждого пикселя, 
	 * заменяется на медиану всех цветов в некоторой окрестности пикселя.
	 * https://en.wikipedia.org/wiki/Median_filter
	 * 
	 * Используйте окно размером 3х3 для не граничных пикселей,
	 * Окно размером 2х2 для угловых и 3х2 или 2х3 для граничных.
	 */
	public static double[,] MedianFilter(double[,] original)
	{
        int width = original.GetLength(0);
        int height = original.GetLength(1);
        double[,] result = new double[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                double[] neighbors = GetNeighbors(original, width, height, x, y);
                Array.Sort(neighbors);
                double median = neighbors[neighbors.Length / 2];
                result[x, y] = median;
            }
        }
        return result;
        // return original;
    }
    private static double[] GetNeighbors(double[,] original, int width, int height, int x, int y)
    {
        int minX = Math.Max(0, x - 1);
        int maxX = Math.Min(width - 1, x + 1);
        int minY = Math.Max(0, y - 1);
        int maxY = Math.Min(height - 1, y + 1);
        int size = (maxX - minX + 1) * (maxY - minY + 1);
        double[] neighbors = new double[size];
        int index = 0;
        for (int i = minX; i <= maxX; i++)
        {
            for (int j = minY; j <= maxY; j++)
            {
                neighbors[index++] = original[i, j];
            }
        }
        Array.Resize(ref neighbors, index); // Отрезаем лишние элементы
        return neighbors;
    }
}
