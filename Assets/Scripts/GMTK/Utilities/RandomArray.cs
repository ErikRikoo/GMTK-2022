using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GMTK.Utilities
{
    [Serializable]
    public class RandomArray<T> : IEnumerable<T>
    {
        [SerializeField] private T[] m_Data;

        public T RandomItem => m_Data[Random.Range(0, m_Data.Length)];
        
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var value in m_Data)
            {
                yield return value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}