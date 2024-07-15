namespace Recognizer;

public static class ThresholdFilterTask
{
	public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
	{
        int width = original.GetLength(0);
        int height = original.GetLength(1);
        double[,] result = new double[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                double pixelValue = original[x, y];
                result[x, y] = (pixelValue > whitePixelsFraction) ? 1.0 : 0.0; // Превращаем в черно-белое
            }
        }
        return result;
        // return new double[original.GetLength(0), original.GetLength(1)];
    }
}
