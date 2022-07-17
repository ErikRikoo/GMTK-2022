using System;
using GMTK.UI.PlayerActions.ActionType;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace GMTK.UI.PlayerActions
{
    public class ActionDisplayer : MonoBehaviour
    {
        [SerializeField] private VoidEvent m_ActionConsumed;
        
        [SerializeField] private Sprite[] m_DiceFacesSprites;
        
        [Header("Prefabs")]
        [SerializeField] private OneActionDisplay m_AttackDisplayPrefab;
        [SerializeField] private OneActionDisplay m_ParryDisplayPrefab;
        [SerializeField] private OneActionDisplay m_HealDisplayPrefab;
        [SerializeField] private OneActionDisplay m_EscapeDisplayPrefab;

        private void Awake()
        {
            m_ActionConsumed.Register(ConsumeFirst);
        }

        public void AddAction(APlayerAction action)
        {
            OneActionDisplay displayToSpawn = action switch
            {
                AttackAction _ => m_AttackDisplayPrefab,
                EscapeAction _ => m_EscapeDisplayPrefab,
                HealAction _ => m_HealDisplayPrefab,
                ParryAction _ => m_ParryDisplayPrefab,
            };

            var display = Instantiate(displayToSpawn, transform);
            
            display.UpdateDisplay(
                action, action.BetType.DisplayType, m_DiceFacesSprites[action.BetType.DiceFace - 1]
                );
        }

        public void Clear()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        public void ConsumeFirst()
        {
            if (transform.childCount == 0)
            {
                return;
            }
            
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}