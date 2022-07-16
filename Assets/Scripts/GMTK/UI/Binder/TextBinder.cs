using System;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace GMTK.UI.Binder
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextBinder : MonoBehaviour
    {
        [SerializeField] private string m_Format = "{0}";
        
        [SerializeField] private IntVariable m_VariableToBind;
        private TextMeshProUGUI m_Text;

        private void Awake()
        {
            m_Text = GetComponent<TextMeshProUGUI>();
            if (m_VariableToBind.Changed)
            {
                m_VariableToBind.Changed.Register(OnChanged);
            }
        }

        private void OnChanged(int _value)
        {
            m_Text.text = String.Format(m_Format, _value);
        }
    }
}