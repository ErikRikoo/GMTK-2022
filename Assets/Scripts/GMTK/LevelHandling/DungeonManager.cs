using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEditorInternal;
using UnityEngine;

namespace GMTK.LevelHandling
{
    public class DungeonManager : MonoBehaviour
    {
        [SerializeField] private Vector3 m_DeltaBetweenRooms;
        
        [SerializeField] private DungeonHolder m_DungeonData;

        [SerializeField] private RoomHolder m_CurrentRoom;
        [SerializeField] private VoidEvent ChangeRoom ;
        [SerializeField] private VoidEvent playerEscape;
        [SerializeField] private VoidEvent end_room_spawn;
        [SerializeField] private GameObject YouWin;
        
        

        private int m_CurrentRoomIndex = 0;

        private int CurrentRoomIndex
        {
            get => m_CurrentRoomIndex;
            set
            {
                if (value == m_DungeonData.Count)
                {
                    YouWin.SetActive(true);
                    Debug.Log("you win");
                }


                else
                {


                    m_CurrentRoomIndex = value;
                    if (m_CurrentRoomIndex >= m_Rooms.Count)
                    {
                        Room r = SpawnRoomAt(m_CurrentRoomIndex);
                        m_Rooms.Add(r);
                        m_CurrentRoom.Room = m_Rooms.Last();
                        end_room_spawn.Raise();
                    }

                    m_CurrentRoom.Room = m_Rooms.Last();

                }
            }
        }

        private Room SpawnRoomAt(int _index)
        {
            Vector3 pos = ComputeRoomSpawnPosition(_index);
            return RoomSpawnRoomAt(_index, pos);
        }

        private Vector3 ComputeRoomSpawnPosition(int _index)
        {
            Room previousRoom = m_Rooms[_index - 1];
            Room currentRoom = m_DungeonData[_index];

            return previousRoom.Exit.position + m_DeltaBetweenRooms - currentRoom.Entry.position;
        }

        private Room RoomSpawnRoomAt(int _index, Vector3 position)
        {
            return Instantiate(m_DungeonData[_index], position, Quaternion.identity, transform);
        }

        private List<Room> m_Rooms = new();

        private void Start()
        {
            playerEscape.Register(PreviousRoom);
            ChangeRoom.Register(NextRoom);
            
            if (m_DungeonData.IsEmpty)
            {
                throw new ArgumentException("Dungeon Data is empty, can't handle the dungeon instantiation");
            }
            
            SpawnFirstRoom();
        }

        private void SpawnFirstRoom()
        {
            Room r = RoomSpawnRoomAt(0, Vector3.zero);
            m_Rooms.Add(r);
            CurrentRoomIndex = 0;
        }

        private bool IsLastRoom => m_CurrentRoomIndex == m_DungeonData.Count - 1;

        [Button]
        public void NextRoom()
        {
            ++CurrentRoomIndex;
        }

        public void PreviousRoom()
        {
            --CurrentRoomIndex;
        }
    }
}