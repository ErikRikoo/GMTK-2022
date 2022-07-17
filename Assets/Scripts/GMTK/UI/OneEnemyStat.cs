using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GMTK.UI
{
    public class OneEnemyStat : MonoBehaviour
    {
        [SerializeField] private GameObject m_IsDeadDisplay;
        [SerializeField] private Image m_Icon;
        [SerializeField] private TextMeshProUGUI m_DamageDisplay;
        
        public void UpdateDisplay(Enemies enemy)
        {
            m_DamageDisplay.text = enemy.damage.ToString();
            m_IsDeadDisplay.SetActive(enemy.IsDead());
            // TODO: Get Icon from enemy
        }
    }
}