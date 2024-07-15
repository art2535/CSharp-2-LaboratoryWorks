using Digger.Architecture;
namespace Digger
{
    internal class Gold : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            // Золото никогда не падает
            return new CreatureCommand() { DeltaX = 0, DeltaY = 0, TransformTo = this };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            // Когда Игрок собирает Золото, ему начисляется 10 очков (через Game.Scores).
            if (conflictedObject is Player)
            {
                Game.Scores += 10;
                return true;
            }
            // Если монстр оказывается в клетке с золотом, золото исчезает.
            if (conflictedObject is Monster)
                return true;
            return false;
        }

        public int GetDrawingPriority()
        {
            return 7;
        }

        public string GetImageFileName()
        {
            return "Gold.png";
        }
    }
}
