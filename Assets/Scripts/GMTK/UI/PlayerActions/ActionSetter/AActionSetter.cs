using GMTK.UI.PlayerActions.ActionType;
using GMTK.UI.PlayerActions.BetTypes.ABetType;
using UnityEngine;

namespace GMTK.UI.PlayerActions.ActionSetter
{
    public abstract class AActionSetter : MonoBehaviour
    {
        [SerializeField] private PlayerActionsDisplay m_PlayerActionsDisplay;
        
        public void DisplayPopUp()
        {
            m_PlayerActionsDisplay.DisplayBet(CreateAction);
        }

        protected abstract APlayerAction CreateAction(ABetType bet);
    }
}