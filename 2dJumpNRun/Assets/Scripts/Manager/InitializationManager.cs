using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class InitializationManager : MonoBehaviour
    {
        public void Initialize()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}

