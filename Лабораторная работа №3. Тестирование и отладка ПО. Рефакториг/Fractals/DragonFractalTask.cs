using System;

namespace Fractals;

internal static class DragonFractalTask
{
    public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
    {
        /*
        Начните с точки (1, 0)
        Создайте генератор рандомных чисел с сидом seed
        
        На каждой итерации:

        1. Выберите случайно одно из следующих преобразований и примените его к текущей точке:

            Преобразование 1. (поворот на 45° и сжатие в sqrt(2) раз):
            x' = (x · cos(45°) - y · sin(45°)) / sqrt(2)
            y' = (x · sin(45°) + y · cos(45°)) / sqrt(2)

            Преобразование 2. (поворот на 135°, сжатие в sqrt(2) раз, сдвиг по X на единицу):
            x' = (x · cos(135°) - y · sin(135°)) / sqrt(2) + 1
            y' = (x · sin(135°) + y · cos(135°)) / sqrt(2)
    
        2. Нарисуйте текущую точку методом pixels.SetPixel(x, y)

        */
        double x = 1;
        double y = 0;
        double dx;
        double dy;
        var random = new Random(seed);
        for (var i = 0; i < iterationsCount; i++)
        {
            var nextNumber = random.Next(2);
            dx = GetX(x, y, nextNumber);
            dy = GetY(x, y, nextNumber);
            x = dx;
            y = dy;
            pixels.SetPixel(x, y);
        }
    }
    private static double GetX(double x, double y, int type)
    {
        if (type == 0)
            return (x * Math.Cos(Math.PI / 4) - y * Math.Sin(Math.PI / 4)) / Math.Sqrt(2);
        else
            return (x * Math.Cos((3 * Math.PI) / 4) - y * Math.Sin((3 * Math.PI) / 4)) / Math.Sqrt(2) + 1;
    }
    private static double GetY(double x, double y, int type)
    {
        if (type == 0)
            return (x * Math.Sin(Math.PI / 4) + y * Math.Cos(Math.PI / 4)) / Math.Sqrt(2);
        else
            return (x * Math.Sin((3 * Math.PI) / 4) + y * Math.Cos((3 * Math.PI) / 4)) / Math.Sqrt(2);
    }
}