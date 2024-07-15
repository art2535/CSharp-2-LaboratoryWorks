using Digger.Architecture;
namespace Digger
{
    internal class Player : ICreature
    // Сделайте класс Player, реализовав ICreature.
    {
        public CreatureCommand Act(int x, int y)
        {
            // Сделайте так, чтобы Игрок шагал в разные стороны в зависимости
            // от нажатой клавиши со стрелкой (Game.KeyPressed).
            int deltaX = 0, deltaY = 0;
            switch (Game.KeyPressed)
            {
                case Avalonia.Input.Key.Left:
                    deltaX = -1;
                    deltaY = 0;
                    break;
                case Avalonia.Input.Key.Right:
                    deltaX = 1;
                    deltaY = 0;
                    break;
                case Avalonia.Input.Key.Up:
                    deltaX = 0;
                    deltaY = -1;
                    break;
                case Avalonia.Input.Key.Down:
                    deltaX = 0;
                    deltaY = 1;
                    break;
            }
            // Убедитесь, что Игрок не покидает пределы игрового поля.
            if (x + deltaX > Game.MapWidth - 1)
                deltaX = 0;
            if (y + deltaY > Game.MapHeight - 1)
                deltaY = 0;
            if (x + deltaX < 0)
                deltaX = 0;
            if (y + deltaY < 0)
                deltaY = 0;
            var next = Game.Map[x + deltaX, y + deltaY];
            if (next is Sack)
            {
                deltaX = 0;
                deltaY = 0;
            }
            return new CreatureCommand() { DeltaX = deltaX, DeltaY = deltaY, TransformTo = this };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Monster || conflictedObject is Sack)
                return true;
            return false;
        }

        public int GetDrawingPriority()
        {
            return 5;
        }

        public string GetImageFileName()
        {
            return "Digger.png";
        }
    }
}
