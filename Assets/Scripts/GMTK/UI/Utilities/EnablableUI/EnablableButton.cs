using System;
using UnityEngine;
using UnityEngine.UI;

namespace GMTK.UI.Utilities.EnablableUI
{
    [RequireComponent(typeof(Button))]
    public class EnablableButton : AEnablableUI
    {
        // TODO: Take care of the original color
        [SerializeField] private float m_DarkenFactor = 0.7f;
        
        private Image m_Image;
        private Button m_Button;

        private void Awake()
        {
            m_Image = GetComponent<Image>();
            m_Button = GetComponent<Button>();
        }

        protected override void OnStateChanged(bool state)
        {
            m_Image.color = state ? Color.white : new Color(m_DarkenFactor, m_DarkenFactor, m_DarkenFactor, 1);
            m_Button.interactable = state;
        }
    }
}