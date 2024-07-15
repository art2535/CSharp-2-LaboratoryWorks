namespace Mazes;

public static class SnakeMazeTask
{
    private static void MoveToRight(Robot robot, int steps)
    {
        for (int i = 1; i < steps; i++)
        {
            robot.MoveTo(Direction.Right);
            if (robot.Finished) return;
        }
    }
    private static void MoveToDown(Robot robot)
    {
        for (int i = 1; i < 3; i++) {
            robot.MoveTo(Direction.Down);
            if (robot.Finished) return;
        }
    }
    private static void MoveToLeft(Robot robot, int steps)
    {
        for (int i = 1; i < steps; i++) {
            robot.MoveTo(Direction.Left);
            if (robot.Finished) return;
        }
    }
    private static void Process(Robot robot, int steps)
    {
        while (true) {
            MoveToRight(robot, steps);
            if (robot.Finished) break;
            MoveToDown(robot);
            if (robot.Finished) break;
            MoveToLeft(robot, steps);
            if (robot.Finished) break;
            MoveToDown(robot);
        }
    }
    public static void MoveOut(Robot robot, int width, int height)
    {
        int needSteps = width - 2;
        Process(robot, needSteps);
    }
}