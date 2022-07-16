using System;
using UnityEngine;

namespace GMTK.UI.Utilities.EnablableUI
{
    public class EnablableMovableUI : AEnablableUI
    {
        [Min(0)]
        [SerializeField] private float m_MovementDuration;
        
        private float m_MovementSpeed; 
        
        private Vector2 m_Target;

        private Vector2 EndOfScreen => new Vector2(Screen.width * 0.5f, -Screen.height * 0.5f);
        
        private RectTransform RectTrans => transform as RectTransform;
        
        private void Awake()
        {
            m_Target = RectTrans.anchoredPosition;
            m_MovementSpeed = 1 / m_MovementDuration;
        }

        private Coroutine m_CurrentCoroutine;

        private float m_CurrentTiming;
        
        protected override void OnStateChanged(bool state)
        {
            gameObject.SetActive(state);
            // TODO: Slide in
            //if(state)
            //m_CurrentCoroutine = StartCoroutine()
        }
    }
}