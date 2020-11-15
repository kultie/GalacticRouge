using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Manager
{
    public abstract class SingletonManager<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T Instance;
        private void Awake()
        {
            Instance = GetComponent<T>();
        }
    }
}