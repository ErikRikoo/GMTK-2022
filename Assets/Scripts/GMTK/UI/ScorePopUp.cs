using System;
using TMPro;
using UnityEngine;

namespace GMTK.UI
{
    public class ScorePopUp : MonoBehaviour
    {
        [SerializeField] private string m_Format = "Score: {0}";
        
        [SerializeField] private TextMeshProUGUI m_Display;

        [SerializeField] private Player_holder m_Player;
        
        private void OnEnable()
        {
            m_Display.text = String.Format(m_Format, m_Player.player.score);
        }
    }
}