using System;
using Digger.Architecture;
namespace Digger
{
    internal class Monster : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            // ищем монстра на карте
            var diggerPosition = FindDiggerPosition(Game.Map);
            if (diggerPosition == null)
            {
                // если монстра нет, диггер не двигается
                return new CreatureCommand { DeltaX = 0, DeltaY = 0 };
            }

            int deltaX = 0, deltaY = 0;

            // определение направления движения от монстра к диггеру
            if (diggerPosition.Item1 > x)
                deltaX = 1; // монстр справа от диггера
            else if (diggerPosition.Item1 < x)
                deltaX = -1; // монстр слева от диггера

            if (diggerPosition.Item2 > y)
                deltaY = 1; // монстр ниже диггера
            else if (diggerPosition.Item2 < y)
                deltaY = -1; // монстр выше диггера

            // проверяем, может ли диггер переместиться в выбранном направлении
            if (deltaX != 0 && CanMove(x + deltaX, y, Game.Map))
            {
                return new CreatureCommand { DeltaX = deltaX, DeltaY = 0 };
            }
            if (deltaY != 0 && CanMove(x, y + deltaY, Game.Map))
            {
                return new CreatureCommand { DeltaX = 0, DeltaY = deltaY };
            }

            // если двигаться в сторону монстра нельзя, диггер стоит на месте
            return new CreatureCommand { DeltaX = 0, DeltaY = 0 };
        }

        private Tuple<int, int> FindDiggerPosition(ICreature[,] map)
        {
            // поиск диггера на карте
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] is Player)
                        return Tuple.Create(i, j);
                }
            }
            return null;
        }

        private bool CanMove(int x, int y, ICreature[,] map)
        {
            // проверка возможности перемещения
            // убедимся, что координаты находятся в пределах карты
            if (x < 0 || x >= map.GetLength(0) || y < 0 || y >= map.GetLength(1))
                return false;

            // может ли монстр переместиться на клетку? не земля, не мешок, не золото, не другой монстр
            var creature = map[x, y];
            return creature == null || (creature is Gold && creature is Player) && creature is Sack;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Monster || conflictedObject is Sack)
                return true;
            return false;
        }

        public int GetDrawingPriority()
        {
            return 4;
        }

        public string GetImageFileName()
        {
            return "Monster.png";
        }
    }
}
