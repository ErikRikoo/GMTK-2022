using UnityEngine;

namespace GMTK.UI.Utilities.EnablableUI
{
    public class EnablableUIList : AEnablableUI
    {
        [SerializeField] private AEnablableUI[] m_Enablable;
        
        
        protected override void OnStateChanged(bool state)
        {
            foreach (var enablable in m_Enablable)
            {
                enablable.State = state;
            }
        }
    }
}