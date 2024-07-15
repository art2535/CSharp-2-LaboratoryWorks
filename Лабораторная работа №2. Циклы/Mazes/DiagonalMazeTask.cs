using System;

namespace Mazes;

public static class DiagonalMazeTask
{
    private static Direction ChooseDirection(int width, int height)
    {
        if (width > height)
            return Direction.Right;
        else
            return Direction.Down;
    }
    private static int ChooseStepsOver(int width, int height)
    {
        int a = width - 2;
        int b = height - 2;
        return Math.Max(a, b) / Math.Min(a, b);
    }
    private static void GoToDirection(Robot robot, Direction dir, int steps)
    {
        for (int i = 0; i < steps; i++) {
            robot.MoveTo(dir);
            if (robot.Finished) break;
        }
    }
    private static Direction ChangeDirection(Direction dir)
    {
        if (dir == Direction.Down)
            return Direction.Right;
        else
            return Direction.Down;
    }
    public static void MoveOut(Robot robot, int width, int height)
    {
        var firstDirection = ChooseDirection(width, height);
        var secondDirection = ChangeDirection(firstDirection);
        int stepsOver = ChooseStepsOver(width, height);
        while (true)
        {
            GoToDirection(robot, firstDirection, stepsOver);
            if (robot.Finished) break;
            GoToDirection(robot, secondDirection, 1);
            if (robot.Finished) break;
        }
    }
}