using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class InitializationSceneManager : MonoBehaviour
    {
        private void Start()
        {
            FindObjectOfType<InitializationManager>().Initialize();
        }
    }
}

