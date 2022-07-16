using System;

namespace GMTK.LevelHandling.Loot
{
    [Serializable]
    public abstract class ALoot
    {
        public abstract string LootInfo
        {
            get;
        }

        public abstract void ExecuteOn(Player _player);
    }
}