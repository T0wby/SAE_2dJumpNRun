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
    [SerializeField] private SO_GameSettings _GameSettings;
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


        _inMenu = false;
    }

    private void Start()
    {
        SetGameSettings();
    }


    #region Level
    public void Resume()
    {
        _mainMenu.SetActive(false);
        _menuBackground.SetActive(false);
        _inMenu = false;
        GameManager.Instance.IsPaused = false;
        GameManager.Instance.PauseGame();
    }

    public void BackToMainMenuScene()
    {
        _inMenu = false;
        GameManager.Instance.IsPaused = false;
        GameManager.Instance.PauseGame();
        SceneManager.LoadSceneAsync("MainMenu");
        SetGameSettings();
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
    #endregion

    #region MainMenu
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("LevelOne");
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
    #endregion

    public void ChangeCoyoteValue(bool value)
    {
        _GameSettings.coyoteToggle = value;
    }

    public void ChangeDoubleJumpValue(bool value)
    {
        _GameSettings.doubleJumpToggle = value;
    }

    public void ChangeWallSlideValue(bool value)
    {
        _GameSettings.wallSlideToggle = value;
    }

    public void ChangeWallJumpValue(bool value)
    {
        _GameSettings.wallJumpToggle = value;
    }

    public void ChangeJumpBufferValue(bool value)
    {
        _GameSettings.jumpBufferToggle = value;
    }

    //private void GetReferences()
    //{
    //    _doubleJumpToggle = GameObject.FindGameObjectWithTag("TGL_DoubleJump").GetComponent<Toggle>();
    //    _coyoteToggle = GameObject.FindGameObjectWithTag("TGL_CoyoteTime").GetComponent<Toggle>();
    //    _jumpBufferToggle = GameObject.FindGameObjectWithTag("TGL_JumpBuffer").GetComponent<Toggle>();
    //    _wallJumpToggle = GameObject.FindGameObjectWithTag("TGL_WallJump").GetComponent<Toggle>();
    //    _wallSlideToggle = GameObject.FindGameObjectWithTag("TGL_WallSlide").GetComponent<Toggle>();

    //    _mainMenu = GameObject.FindGameObjectWithTag("MainMenu");
    //    _optionsMenu = GameObject.FindGameObjectWithTag("OptionsMenu");
    //    _optionsMenu.SetActive(false);
    //    _menuBackground = GameObject.FindGameObjectWithTag("Background_UI");

    //}

    //private void GetReferencesLevelOne()
    //{
    //    _mainMenu = GameObject.FindGameObjectWithTag("MainMenu");
    //    _optionsMenu = GameObject.FindGameObjectWithTag("OptionsMenuGame");
    //    _menuBackground = GameObject.FindGameObjectWithTag("Background_UI");

    //    _mainMenu.SetActive(false);
    //    _optionsMenu.SetActive(false);
    //    _menuBackground.SetActive(false);
    //}

    private void SetGameSettings()
    {
        _coyoteToggle.isOn = _GameSettings.coyoteToggle;
        _doubleJumpToggle.isOn = _GameSettings.doubleJumpToggle;
        _wallSlideToggle.isOn = _GameSettings.wallSlideToggle;
        _wallJumpToggle.isOn = _GameSettings.wallJumpToggle;
        _jumpBufferToggle.isOn = _GameSettings.jumpBufferToggle;
    }
}
