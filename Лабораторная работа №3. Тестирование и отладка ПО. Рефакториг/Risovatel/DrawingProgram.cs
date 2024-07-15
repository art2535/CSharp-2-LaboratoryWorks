using System;
using Avalonia.Media;
using RefactorMe.Common;

namespace RefactorMe
{
    class Risovatel
    {
        static float x, y;
        static IGraphics? graphics = null;

        public static void Initialization(IGraphics newGraphics)
        {
            graphics = newGraphics;
            graphics.Clear(Colors.Black);
        }

        public static void SetPosition(float x0, float y0)
        {
            x = x0;
            y = y0;
        }

        public static void MakeIt(Pen pen, double lenght, double corner, IGraphics? graphics)
        {
            var x1 = (float)(x + lenght * Math.Cos(corner));
            var y1 = (float)(y + lenght * Math.Sin(corner));
            graphics.DrawLine(pen, x, y, x1, y1);
            x = x1;
            y = y1;
        }

        public static void Change(double lenght, double corner)
        {
            x = (float)(x + lenght * Math.Cos(corner)); 
            y = (float)(y + lenght * Math.Sin(corner));
        }
    }
    
    public class ImpossibleSquare
    {
        public static void DrawSide(double corner, double size, double sqrt_2, IGraphics graphics)
        {
            const double PI = Math.PI;
            Risovatel.MakeIt(new Pen(new SolidColorBrush(Colors.Yellow)), size * 0.375f, corner, graphics);
            Risovatel.MakeIt(new Pen(new SolidColorBrush(Colors.Yellow)), size * 0.04f * sqrt_2, corner + PI / 4, graphics);
            Risovatel.MakeIt(new Pen(new SolidColorBrush(Colors.Yellow)), size * 0.375f, corner + PI, graphics);
            Risovatel.MakeIt(new Pen(new SolidColorBrush(Colors.Yellow)), size * 0.375f - size * 0.04f, corner + PI / 2, graphics);
        }

        public static void Draw(int width, int height, double rotationCorner, IGraphics graphics)
        {
            const double PI = Math.PI;
            double sqrt_2 = Math.Sqrt(2);

            Risovatel.Initialization(graphics);

            var size = Math.Min(width, height);

            var diagonalLength = sqrt_2 * (size * 0.375f + size * 0.04f) / 2;
            var x0 = (float)(diagonalLength * Math.Cos(PI / 4 + PI)) + width / 2f;
            var y0 = (float)(diagonalLength * Math.Sin(PI / 4 + PI)) + height / 2f;

            Risovatel.SetPosition(x0, y0);

            DrawSide(0, size, sqrt_2, graphics);
            Risovatel.Change(size * 0.04f, -PI);
            Risovatel.Change(size * 0.04f * sqrt_2, 3 * PI / 4);

            DrawSide(-PI / 2, size, sqrt_2, graphics);
            Risovatel.Change(size * 0.04f, -PI / 2 - PI);
            Risovatel.Change(size * 0.04f * sqrt_2, -PI / 2 + 3 * PI / 4);

            DrawSide(PI, size, sqrt_2, graphics);
            Risovatel.Change(size * 0.04f, PI - PI);
            Risovatel.Change(size * 0.04f * sqrt_2, PI + 3 * PI / 4);

            DrawSide(PI / 2, size, sqrt_2, graphics);
            Risovatel.Change(size * 0.04f, PI / 2 - PI);
            Risovatel.Change(size * 0.04f * sqrt_2, PI / 2 + 3 * PI / 4);
        }
    }
}