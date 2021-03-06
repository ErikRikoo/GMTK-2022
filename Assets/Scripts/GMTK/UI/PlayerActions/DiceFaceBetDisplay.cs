using System;
using System.Collections;
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
            if (state)
            {
                foreach (var diceFace in m_DiceFaces)
                {
                    diceFace.interactable = true;
                    (diceFace.transform as RectTransform).localScale = Vector3.one;
                }
            
                foreach (var animator in m_DiceFacesAnimator)
                {
                    animator.SetBool("HighlightedDice", false);
                    animator.SetBool("SelectedDice", false);
                }
            }

            gameObject.SetActive(state);

        }

        public void UpdateBetDiceFace(int selected, ABetType betType)
        {
            foreach (var diceFace in m_DiceFaces)
            {
                diceFace.interactable = true;
            }

            m_DiceFaces[selected].interactable = false;
            
            for (var i = 0; i < m_DiceFacesAnimator.Length; i++)
            {
                Animator anim = m_DiceFacesAnimator[i];
                bool faceValid = betType.IsFaceValid(i + 1);
                anim.SetBool("HighlightedDice", false);
                anim.SetBool("SelectedDice", false);
                if (i == selected)
                {
                    anim.SetBool("SelectedDice", true);

                    if (faceValid)
                    {
                        anim.SetBool("HighlightedDice", true);
                    }
                }
                else
                {
                    anim.SetBool("SelectedDice", false);

                    if (faceValid)
                    {
                        anim.SetBool("HighlightedDice", true);
                    }
                }
            }
        }
    }
}