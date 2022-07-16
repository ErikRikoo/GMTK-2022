using System.Collections.Generic;
using EditorUtilities.Editor.Attributes.AbstractReference;
using UnityEngine;

namespace GMTK.LevelHandling.Loot
{
    public class RoomLoot : MonoBehaviour
    {
       
        [AbstractReference] 
        [SerializeReference] private ALoot[] m_Loots;

        [SerializeField] private bool m_RandomLoot;

         public IEnumerator<ALoot> Loot {
             get
             {
                 if (m_RandomLoot)
                 {
                     yield return m_Loots[Random.Range(0, m_Loots.Length)];
                 }
                 else
                 {
                     foreach (var loot in m_Loots)
                     {
                         yield return loot;
                     }
                 }
             }
         }
    }
}