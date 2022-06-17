using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _fallMultiplier = 2.5f;
    [SerializeField] private float _lowJumpMultiplier = 2f;
    [SerializeField] private float _wallSlidingSpeed;
    [SerializeField] private float _wallJumpForceX;
    [SerializeField] private float _wallJumpForceY;
    [SerializeField] private float _wallJumpTime = .2f;
    [SerializeField] private float _JumpBufferTime = .5f;
    [SerializeField] private CapsuleCollider2D _feetCollider;
    [SerializeField] private CircleCollider2D _rightCollider;
    [SerializeField] private CircleCollider2D _leftCollider;
    [SerializeField] private LayerMask _groundLayers;
    [SerializeField] private LayerMask _wallLayers;

    private GameManager _gameManager;
    private UIManager _uiManager;
    private Vector2 _moveVal;
    private Rigidbody2D _rb;
    private bool _longJump = false;
    private bool _doubleJump = false;
    private bool _wallSliding = false;
    private bool _wallJumpRight = false;
    private bool _wallJumpLeft = false;
    private bool _jumpBufferCountdown = false;
    private bool _canMove = true;
    private float _coyoteStartTime = .3f;
    private float _coyoteCounter;
    private float _wallJumpCounter;
    private float _jumpBufferCounter;
    private PlayerInputActions _playerControls;
    private InputAction _move;
    private InputAction _jump;
    private InputAction _interact;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerControls = new PlayerInputActions();
        _gameManager = FindObjectOfType<GameManager>();
        _uiManager = FindObjectOfType<UIManager>();

        // Setting the coyote counter to allow jumping when it is off
        //_coyoteCounter = 0.01f;
    }

    private void OnEnable()
    {
        _move = _playerControls.Player.Move;
        _move.Enable();

        _interact = _playerControls.Player.Interact;
        _interact.Enable();

        _jump = _playerControls.Player.Jump;
        _jump.Enable();
    }

    private void OnDisable()
    {
        _move.Disable();
        _jump.Disable();
        _interact.Disable();
    }

    private void Update()
    {
        JumpMechanic();
        Coyote();
        WallSlideCheck();
        WallJumpCheck();
        WallJumpTimer();
        JumpBuffer();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveVal = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && (_coyoteCounter > 0f || _wallJumpRight || _wallJumpLeft))
        {
            if (_gameManager.CanDoubleJump)
            {
                Jump();
                _doubleJump = true;
            }
            else
            {
                Jump();
            }
        }
        else if (context.started && _doubleJump)
        {
            Jump();
            _doubleJump = false;
        }
        else if (context.started && _jumpBufferCounter <= 0 && _gameManager.JumpBufferOn)
        {
            _jumpBufferCounter = _JumpBufferTime;
            _jumpBufferCountdown = true;

            if (_gameManager.CanDoubleJump)
                _doubleJump = true;
        }

        _longJump = context.performed;
    }

    public void OnEscape(InputAction.CallbackContext context)
    {
        if (context.started && !_uiManager.InMenu)
        {
            _gameManager.IsPaused = true;
            _gameManager.PauseGame();
            _uiManager.InMenu = true;
            _uiManager.GameMenuBackground.SetActive(true);
            _uiManager.GameMainMenu.SetActive(true);
        }
    }

    private void Move()
    {
        if (_canMove)
        {
            if (_wallSliding)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Clamp(_rb.velocity.y * Time.fixedDeltaTime, -_wallSlidingSpeed, float.MaxValue) * Time.fixedDeltaTime);
            }
            else
            {
                _rb.velocity = new Vector2(_moveVal.x * _moveSpeed * Time.fixedDeltaTime, _rb.velocity.y);
            }
        }
    }

    private void Jump()
    {
        if (_wallJumpLeft)
        {
            _rb.velocity = new Vector2(_wallJumpForceX * 1, _wallJumpForceY);
            _wallJumpCounter = _wallJumpTime;
        }
        else if (_wallJumpRight)
        {
            _rb.velocity = new Vector2(_wallJumpForceX * -1, _wallJumpForceY);
            _wallJumpCounter = _wallJumpTime;
        }
        else if (_wallSliding)
        {
            return;
        }
        else
        {
            _rb.velocity = Vector2.up * _jumpForce;
        }
    }

    private void JumpMechanic()
    {
        switch (_rb.velocity.y)
        {
            case < 0:
                _rb.velocity += Vector2.up * Physics2D.gravity * ((_fallMultiplier - 1) * Time.deltaTime);
                break;
            case > 0 when !_longJump:
                _rb.velocity += Vector2.up * Physics2D.gravity * ((_lowJumpMultiplier - 1) * Time.deltaTime);
                break;
        }
    }

    private void WallJumpTimer()
    {
        if (_wallJumpCounter <= 0)
        {
            _canMove = true;
        }
        else
        {
            _canMove = false;
            _wallJumpCounter -= Time.deltaTime;
        }
    }

    private void JumpBuffer()
    {
        if (_gameManager.JumpBufferOn)
        {
            if (_jumpBufferCountdown)
            {
                _jumpBufferCounter -= Time.deltaTime;
            }
            else
            {
                _jumpBufferCounter = 0;
            }

            if (_jumpBufferCounter > 0 && IsGrounded())
            {
                Debug.Log("Buffer");
                Jump();
                _jumpBufferCountdown = false;
            }
        }
        
    }

    private bool IsGrounded()
    {
        return _feetCollider.IsTouchingLayers(_groundLayers);
    }

    // Function for the Coyote jump
    private void Coyote()
    {
        // Checking if the player wants to use the Coyote timer
        if (_gameManager.CanCoyoteJump)
        {
            if (IsGrounded())
            {
                _coyoteCounter = _coyoteStartTime;
            }
            else
            {
                _coyoteCounter -= Time.deltaTime;
            }
        }
        else //If it is not true we set the couter depending if we are touching the ground or not to use the logic in the OnJump()
        {
            if (IsGrounded())
            {
                _coyoteCounter = _coyoteStartTime;
            }
            else
            {
                _coyoteCounter = 0f;
            }
        }
    }

    private void WallSlideCheck()
    {

        if (_gameManager.CanWallSlide)
        {
            if (_rightCollider.IsTouchingLayers(_wallLayers) && !IsGrounded() && _moveVal.x > 0)
            {
                _wallSliding = true;
            }
            else if(_leftCollider.IsTouchingLayers(_wallLayers) && !IsGrounded() && _moveVal.x < 0)
            {
                _wallSliding = true;
            }
            else
            {
                _wallSliding = false;
            }
        }
        else
        {
            _wallSliding = false;
        }
    }

    private void WallJumpCheck()
    {
        if (_gameManager.CanWallJump)
        {
            if (_rightCollider.IsTouchingLayers(_wallLayers) && !IsGrounded())
            {
                _wallJumpRight = true;
            }
            else if (_leftCollider.IsTouchingLayers(_wallLayers) && !IsGrounded())
            {
                _wallJumpLeft = true;
            }
            else
            {
                _wallJumpRight = false;
                _wallJumpLeft = false;
            }
        }
        else
        {
            _wallJumpRight = false;
            _wallJumpLeft = false;
        }
    }
}
