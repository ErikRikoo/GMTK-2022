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
            int value = 1;
            float time = 0;
            while (time < m_BetDuration)
            {
                value = Random.Range(1, 7);
                UpdateText(value);
                float nextTime = time + m_RefreshRate;
                while (time < nextTime)
                {
                    yield return null;
                    time += Time.deltaTime;
                }

                
            }
            
            m_Image.gameObject.SetActive(false);

            OnBetFinished(value);

        }

        private void UpdateText(int value)
        {
            m_Image.sprite = m_Faces[value - 1];
        }
    }
}