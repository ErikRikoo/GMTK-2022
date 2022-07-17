using System;
using UnityEngine;

namespace GMTK.Utilities
{
    public class LookAtCamera : MonoBehaviour
    {
        private Transform m_Cam;

        private void Awake()
        {
            m_Cam = Camera.main.transform;
        }

        private void Update()
        {
            transform.LookAt(m_Cam);
        }
    }
}