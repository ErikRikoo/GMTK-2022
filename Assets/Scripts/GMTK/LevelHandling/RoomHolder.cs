using UnityEngine;

namespace GMTK.LevelHandling
{
    [CreateAssetMenu(fileName = "CurrentRoom", menuName = "Level Handling/Room Holder", order = 0)]
    public class RoomHolder : ScriptableObject
    {
        [SerializeField]
        [HideInInspector]
        public Room Room;
    }
}