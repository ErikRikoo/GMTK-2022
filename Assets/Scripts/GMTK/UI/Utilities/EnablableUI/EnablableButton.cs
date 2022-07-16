using System;
using UnityEngine;
using UnityEngine.UI;

namespace GMTK.UI.Utilities.EnablableUI
{
    [RequireComponent(typeof(Button))]
    public class EnablableButton : AEnablableUI
    {
        private Button m_Button;

        private void Awake()
        {
            m_Button = GetComponent<Button>();
        }

        protected override void OnStateChanged(bool state)
        {
            m_Button.enabled = false;
        }
    }
}