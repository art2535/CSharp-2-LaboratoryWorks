using System;

namespace Mazes
{
    public static class PyramidMazeTask
    {
        private static void MoveRight(Robot robot, int width)
        {
            for (int i = 0; i < width - 3; i++) {
                robot.MoveTo(Direction.Right);
                if (robot.Finished) return;
            }
        }
        private static void MoveUp(Robot robot)
        {
            robot.MoveTo(Direction.Up);
            if (robot.Finished) return;
            robot.MoveTo(Direction.Up);
            if (robot.Finished) return;
        }
        private static void MoveLeft(Robot robot, int width)
        {
            for (int i = 0; i < width - 3; i++)
            {
                robot.MoveTo(Direction.Left);
                if (robot.Finished) return;
            }
        }
        public static void MoveOut(Robot robot, int width, int height)
        {
            while (true)
            {
                MoveRight(robot, width);
                if (robot.Finished) break;
                MoveUp(robot);
                if (robot.Finished) break;
                width -= 2;
                MoveLeft(robot, width);
                if (robot.Finished) break;
                MoveUp(robot);
                if (robot.Finished) break;
                width -= 2;
            }
        }
    }
}