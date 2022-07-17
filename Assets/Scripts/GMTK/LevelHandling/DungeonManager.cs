using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GMTK.Utilities.Extensions;
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

        private int m_CurrentRoomIndex = 0;

        private int CurrentRoomIndex
        {
            get => m_CurrentRoomIndex;
            set
            {
                UndisplayRoomAt(m_CurrentRoomIndex);
                m_CurrentRoomIndex = value;

                if (m_CurrentRoomIndex >= m_Rooms.Count)
                {
                    Room r = SpawnRoomAt(m_CurrentRoomIndex);
                    m_Rooms.Add(r);
                }

                DisplayRoomAt(value);
                m_CurrentRoom.Room = m_Rooms.Last();
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

        [Header("Animation")]
        [SerializeField] private AnimationCurve m_AnimationCurve;

        [SerializeField] private float m_Duration;
        
        [SerializeField] private float m_HeightDifference;
        [SerializeField] private float m_DelayBetweenRows;
        
        
        private void DisplayRoomAt(int value)
        {
            AnimateRoom(m_Rooms[value], -m_HeightDifference, 0);
        }

        private void UndisplayRoomAt(int currentRoomIndex)
        {
            AnimateRoom(m_Rooms[currentRoomIndex], 0, -m_HeightDifference);

        }

        private void AnimateRoom(Room room, float startHeightDelta, float endHeightDelta)
        {
            AnimateChild(room, room.m_WallsSouth, startHeightDelta, endHeightDelta);
            AnimateChild(room, room.m_WallsEast, startHeightDelta, endHeightDelta);
            AnimateChild(room, room.m_WallsNorth, startHeightDelta, endHeightDelta);
            AnimateChild(room, room.m_WallsWest, startHeightDelta, endHeightDelta);
            AnimateChild(room, room.m_Floor, startHeightDelta, endHeightDelta);
        }

        private void AnimateChild(Room room, Transform transformToAnimate, float startHeightDelta, float endHeightDelta)
        {
            Vector2 center = room.transform.position.XZ();
            Debug.Log(room.transform.position);
            for (int i = 0; i < transformToAnimate.childCount; i++)
            {
                var child = transformToAnimate.GetChild(i);
                StartCoroutine(c_AnimateObject(child, center, startHeightDelta, endHeightDelta));
            }
        }

        private IEnumerator c_AnimateObject(Transform child, Vector2 center, float startHeightDelta, float endHeightDelta)
        {
            Vector3 start = child.position + new Vector3(0, startHeightDelta, 0);
            Vector3 target = child.position + new Vector3(0, endHeightDelta, 0);;
            float delayToWait = Vector2.Distance(center, child.position.XZ()) * m_DelayBetweenRows;
            yield return new WaitForSeconds(delayToWait);
            float inverseDuration = 1 / m_Duration;
            for(float time = 0; time < m_Duration; time += Time.deltaTime)
            {
                float ratio = time * inverseDuration;
                child.position = Vector3.LerpUnclamped(start, target, m_AnimationCurve.Evaluate(ratio));
                
                yield return null;
            }

            child.position = target;
        }
    }
}