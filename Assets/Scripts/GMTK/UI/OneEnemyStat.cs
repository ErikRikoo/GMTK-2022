using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GMTK.UI
{
    public class OneEnemyStat : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private GameObject m_IsDeadDisplay;
        [SerializeField] private Image m_Icon;
        [SerializeField] private TextMeshProUGUI m_DamageDisplay;
        private Enemies m_Enemy;

        public void UpdateDisplay(Enemies enemy)
        {
            m_Enemy = enemy;
            m_DamageDisplay.text = enemy.damage.ToString();
            m_IsDeadDisplay.SetActive(enemy.IsDead());
            // TODO: Get Icon from enemy
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            m_Enemy.transform.Highlight();
        }


        public void OnPointerExit(PointerEventData eventData)
        {
            m_Enemy.transform.Unhighlight();
        }
    }
}