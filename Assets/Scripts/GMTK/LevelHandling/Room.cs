using System.Collections.Generic;
using UnityEngine;

namespace GMTK.LevelHandling
{
    public class Room : MonoBehaviour
    {
        [SerializeField] private Transform m_Entry;
        [SerializeField] private Transform m_Exit;

        [SerializeField] private List<Enemies> m_Enemies;

        public IEnumerable<Enemies> Enemies => m_Enemies;

        public bool HasEnemies => true;
    }
}