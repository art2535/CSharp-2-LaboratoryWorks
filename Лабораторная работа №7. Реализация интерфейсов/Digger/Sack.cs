using Digger.Architecture;
namespace Digger
{
    internal class Sack : ICreature
    {
        int count = 0;
        public CreatureCommand Act(int x, int y)
        {
            // Если под мешком находится пустое место, он начинает падать.
            int deltaY = 0;
            ICreature transform = this;
            if (y < Game.MapHeight - 1)
            {
                var down = Game.Map[x, y + 1];  // то, что ниже
                // Мешок превращается в Золото, если он падал дольше одной клетки игрового поля и
                // приземлился на Землю, на другой Мешок, на Золото.
                if (count > 1 && (down is Terrain || down is Sack || down is Gold))
                    transform = new Gold();
                if (down == null)
                {
                    deltaY = 1;
                    count++;
                }
                // Мешок может лежать на любой другой сущности (Игрок, Земля, Мешок, Золото, край карты).
                else
                {
                    if (count > 0 && down is Player)
                    {
                        deltaY = 1;
                        Game.Map[x, y + 1] = null;
                    }
                    else
                        count = 0;
                }
            }
            // или на край карты
            if (count > 1 && y == Game.MapHeight - 1)
                transform = new Gold();
            return new CreatureCommand() { DeltaX = 0, DeltaY = deltaY, TransformTo = transform };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Monster)
                return false;
            return true;
        }

        public int GetDrawingPriority()
        {
            return 3;
        }

        public string GetImageFileName()
        {
            return "Sack.png";
        }
    }
}
