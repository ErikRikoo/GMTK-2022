using System;
using EditorUtilities.Editor.Attributes.AbstractReference;
using UnityEngine;
using UnityEngine.Android;

namespace GMTK.LevelHandling.Loot
{
    [Serializable]
    [AbstractNaming("Increase Damage")]
    public class IncreaseDamageLoot : ALoot
    {
        
        [SerializeField] private int m_DamageIncrease;
        
        public override string LootInfo => $"Increase Damage by {m_DamageIncrease}";
        public override void ExecuteOn(Player _player)
        {
            _player.Damage += m_DamageIncrease;
        }
    }
}