using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _optionsMenu;
    [SerializeField] private GameObject _menuBackground;
    [SerializeField] private Toggle _coyoteToggle;
    [SerializeField] private Toggle _doubleJumpToggle;
    [SerializeField] private Toggle _wallSlideToggle;
    [SerializeField] private Toggle _wallJumpToggle;
    [SerializeField] private Toggle _jumpBufferToggle;
    private GameManager _gameManager;
    private bool _inMenu;

    public GameObject MainMenu { get { return _mainMenu; } }
    public GameObject OptionsMenu { get { return _optionsMenu; } }
    public GameObject MenuBackground { get { return _menuBackground; } }
    public bool InMenu { get { return _inMenu; } set { _inMenu = value; } }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        GetReferences();
        _inMenu = false;
    }

    // Level
    public void Resume()
    {
        _mainMenu.SetActive(false);
        _menuBackground.SetActive(false);
        _inMenu = false;
        _gameManager.IsPaused = false;
        _gameManager.PauseGame();
    }

    public void BackToMainMenuScene()
    {
        _inMenu = false;
        _gameManager.IsPaused = false;
        _gameManager.PauseGame();
        SceneManager.LoadSceneAsync("MainMenu");
        GetReferences();
    }

    public void ToOptionsMenu()
    {
        _mainMenu.SetActive(false);
        _optionsMenu.SetActive(true);
    }

    public void BackToGameMenu()
    {
        _optionsMenu.SetActive(false);
        _mainMenu.SetActive(true);
    }
    // Level End

    // MainMenu
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("LevelOne");
        GetReferences();
    }

    //public void ToMainOptionsMenu()
    //{
    //    _mainMenu.SetActive(false);
    //    _optionsMenu.SetActive(true);
    //}

    public void BackToMainMenu()
    {
        _optionsMenu.SetActive(false);
        _mainMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    // MainMenu End

    public void ChangeCoyoteValue(bool value)
    {
        _gameManager.CanCoyoteJump = value;
    }

    public void ChangeDoubleJumpValue(bool value)
    {
        _gameManager.CanDoubleJump = value;

    }

    public void ChangeWallSlideValue(bool value)
    {
        _gameManager.CanWallSlide = value;
    }

    public void ChangeWallJumpValue(bool value)
    {
        _gameManager.CanWallJump = value;
    }

    public void ChangeJumpBufferValue(bool value)
    {
        _gameManager.JumpBufferOn = value;

    }

    private void GetReferences()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _mainMenu = GameObject.FindGameObjectWithTag("MainMenu");
        _optionsMenu = GameObject.FindGameObjectWithTag("OptionsMenu");
        _optionsMenu.SetActive(false);
        _menuBackground = GameObject.FindGameObjectWithTag("Background_UI");

        Toggle[] tglArr = FindObjectsOfType<Toggle>();
        
        foreach (Toggle t in tglArr)
        {
            switch (t.name)
            {
                case "TGL_DoubleJump":
                    _doubleJumpToggle = t;
                    _doubleJumpToggle.isOn = _gameManager.CanDoubleJump;
                    break;
                case "TGL_CoyoteTime":
                    _coyoteToggle = t;
                    _coyoteToggle.isOn = _gameManager.CanCoyoteJump;
                    break;
                case "TGL_JumpBuffer":
                    _jumpBufferToggle = t;
                    _jumpBufferToggle.isOn = _gameManager.JumpBufferOn;
                    break;
                case "TGL_WallJump":
                    _wallJumpToggle = t;
                    _wallJumpToggle.isOn = _gameManager.CanWallJump;
                    break;
                case "TGL_WallSlide":
                    _wallSlideToggle = t;
                    _wallSlideToggle.isOn = _gameManager.CanWallSlide;
                    break;
                default:
                    break;
            }
        }
    }
}
