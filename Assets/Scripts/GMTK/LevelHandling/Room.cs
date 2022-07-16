using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EditorUtilities.Editor.Attributes.AbstractReference;
using GMTK.LevelHandling.Generation;
using GMTK.LevelHandling.Loot;
using GMTK.Utilities.Extensions;
using NaughtyAttributes;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GMTK.LevelHandling
{
    public class Room : MonoBehaviour
    {
        [SerializeField] public Transform Entry;
        [SerializeField] public Transform Exit;

        [SerializeField] private List<Enemies> m_Enemies;

        public IEnumerable<Enemies> Enemies => m_Enemies;

        public bool IsRoomEmpty => m_Enemies.All(e => e.IsDead());

        [SerializeField] private RoomLoot m_Loot;

        private bool m_IsLooted;
        public IEnumerator<ALoot> Loot
        {
            get
            {
                if (m_IsLooted)
                {
                    return null;
                }
                
                m_IsLooted = true;
                return m_Loot.Loot;
            }
        }

#if UNITY_EDITOR
        #region Unity Editor Generation

        [HorizontalLine(color: EColor.Red, order = 0)]
        [SerializeField] private Vector2Int m_Size;

        private int SizeX => m_Size.x - 1;

        [PropertyRange(0, "SizeX")]
        [SerializeField] private int m_EntryPosition;

        private Vector3 WorldEntryPosition => GetExtremity(m_EntryPosition, -1);

        [PropertyRange(0, "SizeX")]
        [SerializeField] private int m_ExitPosition;

        private Vector3 WorldExitPosition => GetExtremity(m_ExitPosition, 1);
        
        private Vector3 GetExtremity(float x, float side = 1)
            => transform.TransformPoint(
                new Vector3(x - m_Size.x * 0.5f + 0.5f, 0, side * (m_Size.y) * 0.5f)
            );

        private float StartX => -m_Size.x * 0.5f;
        private float StartY => -m_Size.y * 0.5f;

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, m_Size.ToFloat().X1Y());
            
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(WorldEntryPosition, 1f);
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(WorldExitPosition, 1f);
        }

        [SerializeField] private LevelGenerationInfo m_GenerationInfo;
        [SerializeField] private Transform m_Floor;
        [SerializeField] private Transform m_WallsSouth;
        [SerializeField] private Transform m_WallsNorth;
        [SerializeField] private Transform m_WallsWest;
        [SerializeField] private Transform m_WallsEast;

        [Sirenix.OdinInspector.Button]
        public void Generate()
        {
            UpdatePositions();
            GenerateItems();
        }

        private void GenerateItems()
        {
            for (int x = 0; x < m_Size.x; ++x)
            {
                for (int z = 0; z < m_Size.y; z++)
                {
                    InstantiateElement(m_GenerationInfo.Tiles.RandomItem, new Vector2Int(x, z), m_Floor);
                }
            }

            for (int x = 0; x < m_Size.x; ++x)
            {
                if (x == m_EntryPosition)
                {
                    // TODO: SpawnDoor
                }
                else
                {
                    InstantiateElement(m_GenerationInfo.Walls.RandomItem, new Vector2Int(x, 0), m_WallsSouth);
                }

                if (x == m_ExitPosition)
                {
                    // TODO: SpawnDoor
                }
                else
                {
                    InstantiateElement(m_GenerationInfo.Walls.RandomItem, new Vector2Int(x, 0), m_WallsNorth);
                }
            }
            
            for (int z = 0; z < m_Size.y; ++z)
            {
                InstantiateElement(m_GenerationInfo.Walls.RandomItem, new Vector2Int(z, 0), m_WallsWest);
                InstantiateElement(m_GenerationInfo.Walls.RandomItem, new Vector2Int(z, 0), m_WallsEast);
            }
        }

        private void InstantiateElement(GameObject element, Vector2Int _pos, Transform _parent)
        {
            var originalPosition = element.transform.position;
            var item = Instantiate(
                element, _parent, false
            );
            item.transform.localPosition = originalPosition + _pos.ToFloat().X0Y();
        }

        private void UpdatePositions()
        {
            Entry.position = WorldEntryPosition;
            Exit.position = WorldExitPosition;

            m_Floor.localPosition = new Vector3(StartX + 0.5f, 0, StartY + 0.5f);

            m_WallsSouth.localPosition = new Vector2(StartX, StartY).X0Y();
            m_WallsNorth.localPosition = new Vector2(StartX, -StartY).X0Y();
            m_WallsWest.localPosition = new Vector2(StartX, StartY).X0Y();
            m_WallsEast.localPosition = new Vector2(-StartX, StartY).X0Y();
        }
        


        #endregion
        #endif
    }
}