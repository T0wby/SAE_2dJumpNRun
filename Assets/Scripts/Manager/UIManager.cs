using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

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
    [SerializeField] private TMP_Text _diamondCount;
    [SerializeField] private TMP_Text _healthCount;
    [SerializeField] private SO_LevelObjects _LevelObjects;
    [SerializeField] private SO_GameSettings _GameSettings;
    private bool _inMenu;

    public UnityEvent<int> onDiamondCountChange;


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
            Destroy(Instance);
            Instance = this;
        }
        _inMenu = false;
        onDiamondCountChange.AddListener(ChangeDiamondCount);
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
        _LevelObjects.activeScenes[0] = null;
        _LevelObjects.activeScenes[1] = null;
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
        GameManager.Instance.LoadingSave = false;
        SceneManager.LoadSceneAsync("LoadingScreen");
        _LevelObjects.activeScenes[0] = "LevelOne";
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

    public void ChangeDiamondCount(int value)
    {
        if (_diamondCount is null)
            return;
        _diamondCount.text = value.ToString();
    }

    public void ChangeLifeCount(int value)
    {
        _healthCount.text = value.ToString();
    }

    private void SetGameSettings()
    {
        _coyoteToggle.isOn = _GameSettings.coyoteToggle;
        _doubleJumpToggle.isOn = _GameSettings.doubleJumpToggle;
        _wallSlideToggle.isOn = _GameSettings.wallSlideToggle;
        _wallJumpToggle.isOn = _GameSettings.wallJumpToggle;
        _jumpBufferToggle.isOn = _GameSettings.jumpBufferToggle;
    }
}
