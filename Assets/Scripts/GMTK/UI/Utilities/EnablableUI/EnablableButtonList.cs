using UnityEngine;
using UnityEngine.UI;

namespace GMTK.UI.Utilities.EnablableUI
{
    public class EnablableButtonList : AEnablableUI
    {
        [SerializeField] private bool m_LoadOnAwake;
        
        [SerializeField] private float m_DarkenFactor = 0.7f;

        [SerializeField] private Button[] m_Buttons;
        [SerializeField] private Image[] m_Images;

        private void Awake()
        {
            if (m_LoadOnAwake)
            {
                m_Buttons = GetComponentsInChildren<Button>();
                m_Images = GetComponentsInChildren<Image>();
            }
        }

        protected override void OnStateChanged(bool state)
        {
            foreach (var button in m_Buttons)
            {
                button.interactable = state;
            }

            foreach (var image in m_Images)
            {
                image.color = state ? Color.white : new Color(m_DarkenFactor, m_DarkenFactor, m_DarkenFactor, 1);
            }
        }
    }
}