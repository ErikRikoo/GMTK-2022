using System;
using EditorUtilities.Editor.Attributes.AbstractReference;
using UnityEngine;

namespace GMTK.LevelHandling.Loot
{
    [Serializable]
    [AbstractNaming("Heal")]
    public class HealLoot : ALoot
    {
        [Min(0)]
        [SerializeField] private int m_HealAmount = 1;

        public override string LootInfo => $"Healing {m_HealAmount}";
        public override void ExecuteOn(Player _player)
        {
            _player.Heal(m_HealAmount);
        }
    }
}