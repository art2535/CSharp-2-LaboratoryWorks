using System;

namespace Rectangles;

public static class RectanglesTask
{
    // Пересекаются ли два прямоугольника (пересечение только по границе также считается пересечением)
    public static bool AreIntersected(Rectangle r1, Rectangle r2)
    {
        // так можно обратиться к координатам левого верхнего угла первого прямоугольника: r1.Left, r1.Top
        return r1.Left <= r2.Right && r1.Right >= r2.Left && r1.Top <= r2.Bottom && r1.Bottom >= r2.Top;
    }
    
    // Площадь пересечения прямоугольников
    public static int IntersectionSquare(Rectangle r1, Rectangle r2)
    {
        int xOverlap = Math.Max(0, Math.Min(r1.Right, r2.Right) - Math.Max(r1.Left, r2.Left));
        int yOverlap = Math.Max(0, Math.Min(r1.Bottom, r2.Bottom) - Math.Max(r1.Top, r2.Top));
        return xOverlap * yOverlap;
    }

    // Если один из прямоугольников целиком находится внутри другого — вернуть номер (с нуля) внутреннего.
    // Иначе вернуть -1
    // Если прямоугольники совпадают, можно вернуть номер любого из них.
    public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
    {
        if (r1.Left >= r2.Left && r1.Top >= r2.Top && r1.Right <= r2.Right && r1.Bottom <= r2.Bottom)
            return 0;
        if (r2.Left >= r1.Left && r2.Top >= r1.Top && r2.Right <= r1.Right && r2.Bottom <= r1.Bottom)
            return 1;
        return -1;
    }
}