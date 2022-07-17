using System;
using GMTK.UI.PlayerActions.BetTypes.ABetType;
using GMTK.UI.Utilities.EnablableUI;
using UnityEngine;
using UnityEngine.UI;

namespace GMTK.UI.PlayerActions
{
    public class DiceFaceBetDisplay : AEnablableUI
    {
        [SerializeField] private Button[] m_DiceFaces;
        [SerializeField] private Animator[] m_DiceFacesAnimator;

        public int Count => m_DiceFaces.Length;

        public Button this[int i] => m_DiceFaces[i];

        protected override void OnStateChanged(bool state)
        {
            gameObject.SetActive(state);
        }

        public void UpdateBetDiceFace(int selected, ABetType betType)
        {
            foreach (var diceFace in m_DiceFaces)
            {
                diceFace.interactable = true;
            }

            m_DiceFaces[selected].interactable = false;
            
            // for (var i = 0; i < m_DiceFacesAnimator.Length; i++)
            // {
            //     Animator anim = m_DiceFacesAnimator[i];
            //     string triggerName;
            //     if (i == selected)
            //     {
            //         triggerName = "SelectedDice";
            //     }
            //     if (betType.IsFaceValid(i + 1))
            //     {
            //         triggerName = "HighlightedDice";
            //     }
            //     else
            //     {
            //         triggerName = "Normal";
            //     }
            //     
            //     anim.SetTrigger(triggerName);
            // }
        }
    }
}