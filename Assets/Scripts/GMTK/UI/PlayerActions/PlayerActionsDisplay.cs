using System;
using System.Collections;
using System.Collections.Generic;
using GMTK.UI.PlayerActions.ActionType;
using GMTK.UI.PlayerActions.BetTypes.ABetType;
using GMTK.UI.Utilities.EnablableUI;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using Void = UnityAtoms.Void;

namespace GMTK.UI.PlayerActions
{
    public class PlayerActionsDisplay : MonoBehaviour
    {
        [SerializeField] private RectTransform m_DebugPlace;
        [SerializeField] private TextMeshProUGUI m_PrefabText;
        
        
        
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

        public bool HasPlayerActionPoints => CurrentPlayer.number_action > 0;

        public Player CurrentPlayer => m_CurrentPlayer.player;
        
        private void Awake()
        {
            m_StartPlayerTurn.Register(OnStartPlayerTurn);
            m_EndOfTurn.Register(OnEndOfTurn);
        }

        private void Start()
        {
            IsPlayerPlaying = false;
            m_BetPopUp.State = false;
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
            if (!HasPlayerActionPoints)
            {
                // TODO: Can't play
                m_ExecuteActions.Raise();
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
                CurrentPlayer.number_action--;
                AddAction(OnValidate(m_BetPopUp.BetType));
                
                if (!HasPlayerActionPoints)
                {
                    EndPlayerChoice();
                }
            }, () => {});
        }
        
        public void AddAction(APlayerAction action)
        {
            CurrentPlayer.AddAction(action);
            var text = Instantiate(m_PrefabText, m_DebugPlace);
            text.text = $"{action.GetType().Name} - {action.BetType.GetType().Name} - {action.BetType.DiceFace}";
        }
        
        private void OnEndOfTurn()
        {
            Debug.Log("End of Turn in PlayerActionsDisplay");
            for (int i = 1; i < m_DebugPlace.childCount; i++)
            {
                Destroy(m_DebugPlace.GetChild(i).gameObject);
            }   
        }
    }
}