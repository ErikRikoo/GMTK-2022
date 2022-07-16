using System;
using System.Collections;
using System.Collections.Generic;
using GMTK.UI.PlayerActions.ActionType;
using GMTK.UI.PlayerActions.BetTypes.ABetType;
using GMTK.UI.Utilities.EnablableUI;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace GMTK.UI.PlayerActions
{
    public class PlayerActionsDisplay : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] private VoidEvent m_StartPlayerTurn;
        [SerializeField] private VoidEvent m_EndOfTurn;
        [SerializeField] private VoidEvent m_ExecuteActions;
        
        [Header("Variables")] 
        [SerializeField] private Player_holder m_CurrentPlayer;

        [Header("Scene Objects")]
        [SerializeField] private AEnablableUI m_EndTurnButton;
        [SerializeField] private AEnablableUI m_ButtonActions;
        [SerializeField] private BetPopUp m_BetPopUp;

        public bool HasPlayerActionPoints => CurrentPlayer.number_action == 0;

        public Player CurrentPlayer => m_CurrentPlayer.player;
        
        private void Awake()
        {
            m_StartPlayerTurn.Register(OnStartPlayerTurn);
        }

        private void Start()
        {
            IsPlayerPlaying = false;
            m_BetPopUp.State = false;
            StartCoroutine(c_Test());
        }

        private IEnumerator c_Test()
        {
            yield return new WaitForSeconds(1f);
            OnStartPlayerTurn();
        }

        public bool IsPlayerPlaying
        {
            set
            {
                m_EndTurnButton.State = value;
                m_ButtonActions.State = value;
            }
        }

        private void OnStartPlayerTurn()
        {
            if (HasPlayerActionPoints)
            {
                // TODO: Can't play
                m_EndOfTurn.Raise();
            }
            else
            {
                IsPlayerPlaying = true;
            }
        }

        public void EndPlayerChoice()
        {
            IsPlayerPlaying = false;
            m_ExecuteActions.Raise();
        }

        public void DisplayBet(Func<ABetType, APlayerAction> OnValidate)
        {
            m_BetPopUp.Display(() =>
            {
                if (!HasPlayerActionPoints)
                {
                    EndPlayerChoice();
                }

                CurrentPlayer.number_action--;
                AddAction(OnValidate(m_BetPopUp.BetType));
            }, () => {});
        }
        
        public void AddAction(APlayerAction action)
        {
            CurrentPlayer.AddAction(action);
        }
    }
}