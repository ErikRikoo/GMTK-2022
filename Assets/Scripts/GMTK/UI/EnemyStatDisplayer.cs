using System;
using GMTK.LevelHandling;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace GMTK.UI
{
    public class EnemyStatDisplayer : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] private VoidEvent m_ChangeRoom;
        [SerializeField] private VoidEvent m_EscapeRoom;

        [Header("Variables")]
        [SerializeField] private RoomHolder m_CurrentRoom;

        [Header("Prefab")]
        [SerializeField] private OneEnemyStat m_EnemyStatPrefab;

        private void Awake()
        {
            m_ChangeRoom.Register(OnRoomChanged);
            m_EscapeRoom.Register(OnRoomChanged);
        }

        public void OnRoomChanged()
        {
            Clear();
            foreach (var enemy in m_CurrentRoom.Room.Enemies)
            {
                var display = Instantiate(m_EnemyStatPrefab, transform);
                display.UpdateDisplay(enemy);
            }
        }

        private void Clear()
        {
            for (int i = 0; i < transform.childCount; ++i)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
}