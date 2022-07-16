using System;
using UnityEngine;

namespace GMTK.UI.Utilities.EnablableUI
{
    public abstract class AEnablableUI : MonoBehaviour
    {
        
        
        [SerializeField] private bool m_DefaultState;
        
        public bool m_State;
        
        public bool State
        {
            get => m_State;
            set
            {
                m_State = value;
                OnStateChanged(m_State);
            }
        }

        protected abstract void OnStateChanged(bool state);

        protected void Start()
        {
            State = m_DefaultState;
        }
    }
}