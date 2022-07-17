using System;
using System.Collections;
using System.Collections.Generic;
using GMTK.UI.PlayerActions.ActionType;
using GMTK.UI.PlayerActions.BetTypes.ABetType;
using GMTK.Utilities.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace GMTK.UI.PlayerActions.ActionSetter
{
    public class AttackActionSetter : AActionSetter, IPointerUpHandler, IPointerDownHandler
    {
        private Camera m_Camera;
        private RectTransform rectTransform;
        
        private Vector3 m_OriginalPosition;
        private Vector2 m_Delta;
        private Enemies m_Enemy;

        private Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        private void Awake()
        {
            m_Camera = Camera.main;
            rectTransform = transform as RectTransform;
            m_OriginalPosition = Position;
        }

        protected override APlayerAction CreateAction(ABetType bet)
        {
            var action = new AttackAction()
            {
                BetType = bet,
                Enemy = m_Enemy
            };

            m_Enemy = null;
            return action;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            StopAllCoroutines();
            Position = m_OriginalPosition;
            CheckIfEnemyAndTriggerPopUp();
        }

        private void CheckIfEnemyAndTriggerPopUp()
        {
            var ray = m_Camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent(out m_Enemy))
                {
                    DisplayPopUp();
                }
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            m_Delta = transform.position.XY() - Mouse.current.position.ReadValue();
            StartCoroutine(c_Drag());
        }

        private IEnumerator c_Drag()
        {
            while (true)
            {
                yield return null;
                UpdatePositionBasedOnMouse();
            }
        }

        private void UpdatePositionBasedOnMouse()
        {
            Position = Mouse.current.position.ReadValue() + m_Delta;
        }
    }
}