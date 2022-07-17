using UnityEngine;

namespace GMTK.UI.Utilities.EnablableUI
{
    public class EnablableComponent : AEnablableUI
    {
        [SerializeField] private MonoBehaviour m_Component;
        
        
        protected override void OnStateChanged(bool state)
        {
            m_Component.enabled = state;
        }
    }
}