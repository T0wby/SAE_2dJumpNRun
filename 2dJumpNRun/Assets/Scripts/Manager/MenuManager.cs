using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Manager
{

    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance { get; private set; }

        [SerializeField] private GameObject _mainMenu;
        [SerializeField] private GameObject _optionsMenu;
        [SerializeField] private Toggle _coyoteToggle;
        [SerializeField] private Toggle _doubleJumpToggle;
        [SerializeField] private Toggle _wallSlideToggle;
        [SerializeField] private Toggle _wallJumpToggle;

        public void Initialize()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }

        public void PlayGame()
        {
            SceneManager.LoadScene("LevelOne");
        }

        public void ToMainOptionsMenu()
        {
            _mainMenu.SetActive(false);
            _optionsMenu.SetActive(true);
        }

        public void BackToMainMenu()
        {
            _optionsMenu.SetActive(false);
            _mainMenu.SetActive(true);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
