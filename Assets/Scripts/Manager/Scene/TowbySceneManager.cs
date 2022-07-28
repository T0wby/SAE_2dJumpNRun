using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Manager
{
    public class TowbySceneManager : MonoBehaviour
    {
        public static TowbySceneManager instance { get; private set; }
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
                return;
            }
        }

        public void LoadLevelTwo()
        {
            
        }
    }
}
