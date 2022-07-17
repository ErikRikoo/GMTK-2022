using System.Collections.Generic;
using UnityEngine;

namespace GMTK.LevelHandling
{
    [CreateAssetMenu(fileName = "DungeonHolder", menuName = "Level Handling/Dungeon Holder", order = 0)]
    public class DungeonHolder : ScriptableObject
    {
        [SerializeField] private List<Room> m_Rooms;

        public int Count => m_Rooms.Count;
        public bool IsEmpty => Count <= 0;

        public Room this[int _index] => m_Rooms[_index];
        
    }
}