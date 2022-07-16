using UnityEngine;

namespace GMTK.LevelHandling
{
    [CreateAssetMenu(fileName = "CurrentRoom", menuName = "Level Handling/Room Holder", order = 0)]
    public class RoomHolder : ScriptableObject
    {
        [SerializeField]
        public Room Room;
    }
}