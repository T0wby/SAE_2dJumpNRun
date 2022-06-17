using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject _gameMainMenu;
    [SerializeField] private GameObject _gameOptionsMenu;
    [SerializeField] private GameObject _gameMenuBackground;
    [SerializeField] private Toggle _coyoteToggle;
    [SerializeField] private Toggle _doubleJumpToggle;
    [SerializeField] private Toggle _wallSlideToggle;
    [SerializeField] private Toggle _wallJumpToggle;
    [SerializeField] private Toggle _jumpBufferToggle;
    private GameManager _gameManager;
    private bool _inMenu;

    public GameObject GameMainMenu { get { return _gameMainMenu; } }
    public GameObject GameOptionsMenu { get { return _gameOptionsMenu; } }
    public GameObject GameMenuBackground { get { return _gameMenuBackground; } }
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
        _gameManager = FindObjectOfType<GameManager>();
        _inMenu = false;
        _coyoteToggle.isOn = _gameManager.CanCoyoteJump;
        _doubleJumpToggle.isOn = _gameManager.CanDoubleJump;
        _wallSlideToggle.isOn = _gameManager.CanWallSlide;
        _wallJumpToggle.isOn = _gameManager.CanWallJump;
        _jumpBufferToggle.isOn = _gameManager.JumpBufferOn;
    }

    public void Resume()
    {
        _gameMainMenu.SetActive(false);
        _gameMenuBackground.SetActive(false);
        _inMenu = false;
        _gameManager.IsPaused = false;
        _gameManager.PauseGame();
    }

    public void BackToMainMenu()
    {
        _inMenu = false;
        _gameManager.IsPaused = false;
        _gameManager.PauseGame();
        SceneManager.LoadScene("MainMenu");
    }

    public void ToOptionsMenu()
    {
        _gameMainMenu.SetActive(false);
        _gameOptionsMenu.SetActive(true);
    }

    public void BackToGameMenu()
    {
        _gameOptionsMenu.SetActive(false);
        _gameMainMenu.SetActive(true);
    }

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
}
