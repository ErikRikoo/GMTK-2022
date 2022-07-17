using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

namespace GMTK.UI.Binder
{
    [RequireComponent(typeof(Slider))]
    public class SliderBinder : MonoBehaviour
    {
        [SerializeField] private IntPairEvent m_Event;
        private Slider m_Slider;

        private void Awake()
        {
            m_Slider = GetComponent<Slider>();
            m_Event.Register(OnChanged);
        }

        private void OnChanged(IntPair _value)
        {
            m_Slider.minValue = 0;
            m_Slider.maxValue = _value.Item2;
            m_Slider.value = _value.Item1;
        }
    }
}