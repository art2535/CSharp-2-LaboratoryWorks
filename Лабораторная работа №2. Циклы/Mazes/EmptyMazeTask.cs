using Microsoft.CodeAnalysis.Operations;

namespace Mazes;

public static class EmptyMazeTask
{
    private static void MoveToLeft(Robot robot, int width)
    {
        for (int i = 1; i < width - 2; i++)
        {
            robot.MoveTo(Direction.Left);
            if (robot.Finished) return;
        }
    }
    private static void MoveToDown(Robot robot, int height, int width)
    {
        for (int i = 1; i < height - 2; i++)
        {
            robot.MoveTo(Direction.Down);
            if (robot.Finished) return;
        }
        MoveToLeft(robot, width);
    }
    public static void MoveOut(Robot robot, int width, int height)
    {
        for (int i = 1; i < width - 2; i++) {
            robot.MoveTo(Direction.Right);
            if (robot.Finished) return;
        }
        MoveToDown(robot, height, width);
    }
}