using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class MenuSceneManager : MonoBehaviour
    {
        public static MenuSceneManager instance { get; private set; }
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

        private void Start()
        {
            FindObjectOfType<MenuManager>().Initialize();
        }
    }
}
