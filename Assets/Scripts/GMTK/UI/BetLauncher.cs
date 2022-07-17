using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace GMTK.UI
{
    public class BetLauncher : MonoBehaviour
    {
        [SerializeField] private Image m_Image;

        [SerializeField] private Sprite[] m_Faces;


        [SerializeField] private float m_BetDuration;
        [PropertyRange(0, "m_BetDuration")]
        [SerializeField] private float m_RefreshRate;
        
        
        public void LaunchBet(Action<int> OnBetFinished)
        {
            StartCoroutine(c_Bet(OnBetFinished));
        }

        public IEnumerator c_Bet(Action<int> OnBetFinished)
        {
            m_Image.gameObject.SetActive(true);
            int value;
            float time = 0;
            while (time < m_BetDuration)
            {
                value = Random.Range(1, 7);
                UpdateText(value);
                time += m_RefreshRate;
                yield return new WaitForSeconds(m_RefreshRate);
            }
            
            value = Random.Range(1, 7);
            UpdateText(value);
            OnBetFinished(value);
            m_Image.gameObject.SetActive(false);

        }

        private void UpdateText(int value)
        {
            m_Image.sprite = m_Faces[value - 1];
        }
    }
}