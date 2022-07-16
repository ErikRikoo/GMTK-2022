using System;
using GMTK.UI.Utilities.EnablableUI;
using UnityEngine;
using UnityEngine.UI;

namespace GMTK.UI.Utilities
{
    public class PopUp : EnablableMovableUI
    {
        private Action m_OnValidate;
        private Action m_OnCancel;

        public void Display(Action onValidate, Action onCancel)
        {
            m_OnValidate = onValidate;
            m_OnCancel = onCancel;
            State = true;
        }

        public void Cancel()
        {
            State = false;
            m_OnCancel?.Invoke();
        }

        public void Validate()
        {
            State = false;
            m_OnValidate?.Invoke();
        }
    }
}