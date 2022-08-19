using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.Audio;
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
    [SerializeField] private AudioMixer _volMixer;
    [SerializeField] private Slider _volSlider;
    [SerializeField] private TMP_Dropdown displayDropdown;
    [SerializeField] private Toggle _coyoteToggle;
    [SerializeField] private Toggle _doubleJumpToggle;
    [SerializeField] private Toggle _wallSlideToggle;
    [SerializeField] private Toggle _wallJumpToggle;
    [SerializeField] private Toggle _jumpBufferToggle;
    [SerializeField] private TMP_Text _diamondCount;
    [SerializeField] private TMP_Text _healthCount;
    [SerializeField] private SO_LevelObjects _levelObjects;
    [SerializeField] private SO_LevelObjects _defaultLevelObjects;
    [SerializeField] private SO_GameSettings _gameSettings;
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
        _levelObjects.activeScenes[0] = null;
        _levelObjects.activeScenes[1] = null;
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
        SetDefaultSettings();
        SceneManager.LoadSceneAsync("LoadingScreen");
        _levelObjects.activeScenes[0] = "LevelOne";
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

    #region Options
    public void ChangeCoyoteValue(bool value)
    {
        _gameSettings.coyoteToggle = value;
    }

    public void ChangeDoubleJumpValue(bool value)
    {
        _gameSettings.doubleJumpToggle = value;
    }

    public void ChangeWallSlideValue(bool value)
    {
        _gameSettings.wallSlideToggle = value;
    }

    public void ChangeWallJumpValue(bool value)
    {
        _gameSettings.wallJumpToggle = value;
    }

    public void ChangeJumpBufferValue(bool value)
    {
        _gameSettings.jumpBufferToggle = value;
    }

    public void SetVolume(float volume)
    {
        _volMixer.SetFloat("volume", volume);
        _gameSettings.volume = volume;
    }

    public void SetDisplayMode()
    {
        switch (displayDropdown.value)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
            case 2:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            default:
                break;
        }
        _gameSettings.displayMode = displayDropdown.value;
    }

    private void SetDisplayMode(int value)
    {
        switch (value)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
            case 2:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            default:
                break;
        }
    }
    #endregion

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
        _coyoteToggle.isOn = _gameSettings.coyoteToggle;
        _doubleJumpToggle.isOn = _gameSettings.doubleJumpToggle;
        _wallSlideToggle.isOn = _gameSettings.wallSlideToggle;
        _wallJumpToggle.isOn = _gameSettings.wallJumpToggle;
        _jumpBufferToggle.isOn = _gameSettings.jumpBufferToggle;
        SetDisplayMode(_gameSettings.displayMode);
        _volMixer.SetFloat("volume", _gameSettings.volume);
        _volSlider.value = _gameSettings.volume;
    }

    private void SetDefaultSettings()
    {
        _levelObjects.playerPosition = _defaultLevelObjects.playerPosition;
        _levelObjects.playerRotation = _defaultLevelObjects.playerRotation;
        _levelObjects.health = _defaultLevelObjects.health;
        _levelObjects.diamondCount = _defaultLevelObjects.diamondCount;
        _levelObjects.leverPulled = _defaultLevelObjects.leverPulled;
        _levelObjects.doorOpen = _defaultLevelObjects.doorOpen;
        _levelObjects.activeScenes = _defaultLevelObjects.activeScenes;
    }
}
