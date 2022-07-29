using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private bool _canDoubleJump;
    private bool _canCoyoteJump;
    private bool _canWallSlide;
    private bool _canWallJump;
    private bool _jumpBufferOn;
    private bool _isPaused;
    private int _diamondCount = 0;

    [SerializeField] private SO_LevelObjects _LevelObjects;


    public UnityEvent<int> onDiamondCountChange;

    public bool CanDoubleJump { get { return _canDoubleJump; } set { _canDoubleJump = value; } }
    public bool CanCoyoteJump { get { return _canCoyoteJump; } set { _canCoyoteJump = value; } }
    public bool IsPaused { get { return _isPaused; } set { _isPaused = value; } }
    public bool CanWallSlide { get { return _canWallSlide; } set { _canWallSlide = value; } }
    public bool CanWallJump { get { return _canWallJump; } set { _canWallJump = value; } }
    public bool JumpBufferOn { get { return _jumpBufferOn; } set { _jumpBufferOn = value; } }
    public int DiamondCount { get { return _diamondCount; } set { _diamondCount = value; onDiamondCountChange.Invoke(_diamondCount); } }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _canDoubleJump = true;
        _canCoyoteJump = true;
        _canWallSlide = true;
        _canWallJump = true;
        _jumpBufferOn = true;
        _isPaused = true;

        onDiamondCountChange.AddListener(UIManager.Instance.ChangeDiamondCount);

        DontDestroyOnLoad(this.gameObject);
    }

    public void PauseGame()
    {
        if (_isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void SaveDiamonds()
    {
        _LevelObjects.diamondCount = _diamondCount;
    }
}
