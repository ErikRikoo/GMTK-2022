using System;
using GMTK.UI.PlayerActions.BetTypes.ABetType;
using GMTK.UI.Utilities;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GMTK.UI.PlayerActions
{
    public class BetPopUp : PopUp
    {
        [SerializeField] private string m_RiskFormat = "Risk: {0}%";
        
        [SerializeField] private TextMeshProUGUI m_RiskDisplay;
        
        
        [SerializeField] private Button m_ButtonLowerBetType;
        [SerializeField] private Button m_ButtonGreaterBetType;
        [SerializeField] private Button m_ButtonEqualBetType;

        [SerializeField] private Button[] m_DiceFaces;

        private int m_CurrentlySelectedFace = -1;
        

        private ABetType m_BetType;
        
        public ABetType BetType => m_BetType;

        public new void Start()
        {
            base.Start();
            m_ButtonLowerBetType.onClick.AddListener(
                GetButtonEventFor(m_ButtonLowerBetType, () => new LowerBetType())
                );
            m_ButtonGreaterBetType.onClick.AddListener(
                GetButtonEventFor(m_ButtonGreaterBetType, () => new GreaterBetType())
                );
            m_ButtonEqualBetType.onClick.AddListener(
                GetButtonEventFor(m_ButtonEqualBetType, () => new EqualBetType())
                );
            
            for (var i = 0; i < m_DiceFaces.Length; i++)
            {
                int index = i;
                m_DiceFaces[i].onClick.AddListener(() =>
                {
                    m_CurrentlySelectedFace = index + 1;
                    UpdateBetDiceFace();
                });
            }
        }

        private UnityAction GetButtonEventFor(Button button, Func<ABetType> _bet)
        {
            return () =>
            {
                m_ButtonLowerBetType.interactable = true;
                m_ButtonGreaterBetType.interactable = true;
                m_ButtonEqualBetType.interactable = true;
                button.interactable = false;

                m_BetType = _bet();
                UpdateBetDiceFace();
            };
        }

        private void UpdateBetDiceFace()
        {
            if (m_CurrentlySelectedFace == -1)
            {
                return;
            }
            else
            {
                if (m_BetType != null)
                {
                    m_BetType.DiceFace = m_CurrentlySelectedFace;
                    m_RiskDisplay.text = String.Format(m_RiskFormat, (int)(m_BetType.Risk * 100));
                }
            }
        }
    }
}