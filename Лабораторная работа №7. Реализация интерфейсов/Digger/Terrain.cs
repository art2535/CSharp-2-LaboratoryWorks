using Digger.Architecture;

namespace Digger
{
    internal class Terrain : ICreature
    // Сделайте класс Terrain, реализовав ICreature. Сделайте так, чтобы он ничего не делал.
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand { DeltaX = 0, DeltaY = 0, TransformTo = this };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            // Сделайте так, чтобы Земля исчезала в тех местах, где прошел Игрок.
            return conflictedObject is Player;
        }

        public int GetDrawingPriority()
        {
            return 10;
        }

        public string GetImageFileName()
        {
            return "Terrain.png";
        }
    }
}
