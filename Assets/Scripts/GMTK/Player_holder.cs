using UnityEngine;

namespace GMTK
{
    [CreateAssetMenu(fileName = "player", menuName = "current_player", order = 0)]
    public class Player_holder : ScriptableObject
    {
        public Player player;
    }
}