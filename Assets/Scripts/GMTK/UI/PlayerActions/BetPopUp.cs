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

        [SerializeField] private DiceFaceBetDisplay m_DiceFacesDisplay;
        
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
            
            for (var i = 0; i < m_DiceFacesDisplay.Count; i++)
            {
                int index = i;
                m_DiceFacesDisplay[i].onClick.AddListener(() =>
                {
                    m_CurrentlySelectedFace = index;
                    UpdateBetDiceFace();
                });
            }

            SetSecondSpaceUIState();
        }



        private UnityAction GetButtonEventFor(Button button, Func<ABetType> _bet)
        {
            return () =>
            {
                ResetTypeButtons();
                button.interactable = false;

                m_BetType = _bet();
                SetSecondSpaceUIState(true);

                UpdateBetDiceFace();
            };
        }

        private void ResetTypeButtons()
        {
            m_ButtonLowerBetType.interactable = true;
            m_ButtonGreaterBetType.interactable = true;
            m_ButtonEqualBetType.interactable = true;
        }
        
        private void SetSecondSpaceUIState(bool state = false)
        {
            m_DiceFacesDisplay.State = state;
            m_RiskDisplay.gameObject.SetActive(state);
            m_RiskDisplay.text = "";
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
                    m_BetType.DiceFace = m_CurrentlySelectedFace + 1;
                    m_DiceFacesDisplay.UpdateBetDiceFace(m_CurrentlySelectedFace, m_BetType);

                    m_RiskDisplay.text = String.Format(m_RiskFormat, (int)(m_BetType.Risk * 100));
                }
            }
        }

        public override void Cancel()
        {
            ResetPopUp();
            base.Cancel();
        }

        public override void Validate()
        {
            ResetPopUp();
            base.Validate();
        }
        
        private void ResetPopUp()
        {
            ResetTypeButtons();
            ResetTypeButtons();
            m_BetType = null;
        }
    }
}