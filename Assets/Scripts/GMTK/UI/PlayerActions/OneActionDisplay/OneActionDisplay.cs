using System.Data;
using System.Net.Mime;
using GMTK.UI.PlayerActions.ActionType;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GMTK.UI.PlayerActions
{
    public class OneActionDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_Type;
        [SerializeField] private Image m_Face;
        protected APlayerAction m_Action;

        public void UpdateDisplay(APlayerAction aPlayerAction, string type, Sprite face)
        {
            m_Action = aPlayerAction;
            m_Type.text = type;
            m_Face.sprite = face;
        }
    }
}