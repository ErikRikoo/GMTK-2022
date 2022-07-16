using UnityEngine;
using UnityEngine.UI;

namespace GMTK.UI.Utilities.EnablableUI
{
    public class EnablableButtonList : AEnablableUI
    {
        [SerializeField] private bool m_LoadOnAwake;
        
        [SerializeField] private Button[] m_Buttons;

        private void Awake()
        {
            if (m_LoadOnAwake)
            {
                m_Buttons = GetComponentsInChildren<Button>();
            }
        }

        protected override void OnStateChanged(bool state)
        {
            foreach (var button in m_Buttons)
            {
                button.interactable = state;
            }
        }
    }
}