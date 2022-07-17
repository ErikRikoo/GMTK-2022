using GMTK.Utilities;
using UnityEngine;

namespace GMTK.LevelHandling.Generation
{
    [CreateAssetMenu(fileName = "LevelGenerationInfo", menuName = "Level Handling/Generation/Info", order = 0)]
    public class LevelGenerationInfo : ScriptableObject
    {
        [SerializeField] public RandomArray<GameObject> Walls;
        [SerializeField] public RandomArray<GameObject> Tiles;
        [SerializeField] public RandomArray<GameObject> Doors;
        
    }
}