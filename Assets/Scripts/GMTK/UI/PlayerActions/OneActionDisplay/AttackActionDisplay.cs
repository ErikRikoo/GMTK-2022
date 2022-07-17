using GMTK.UI.PlayerActions.ActionType;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GMTK.UI.PlayerActions
{
    public class AttackActionDisplay : OneActionDisplay, IPointerEnterHandler, IPointerExitHandler
    {
        private AttackAction Attack => m_Action as AttackAction;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            Attack.Enemy.transform.Highlight();
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            Attack.Enemy.transform.Unhighlight();
        }
    }
}