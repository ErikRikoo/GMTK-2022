using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GMTK.LevelHandling
{
    public class Room : MonoBehaviour
    {
        [SerializeField] public Transform Entry;
        [SerializeField] public Transform Exit;

        [SerializeField] private List<Enemies> m_Enemies;

        public IEnumerable<Enemies> Enemies => m_Enemies;

        public bool IsRoomEmpty => m_Enemies.All(e => e.IsDead());
    }
}