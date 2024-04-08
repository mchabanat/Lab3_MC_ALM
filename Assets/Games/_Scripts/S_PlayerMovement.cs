using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_PlayerMovement : MonoBehaviour
{
    //Inputs
    private IA_PlayerController _inputs = null;

    //Movement
    [Header("Movement")]
    private Rigidbody _rb = null;
    private Vector2 _moveDir = Vector2.zero;
    [SerializeField] private float _speed = 5f;

    [SerializeField] private float _groundDrag;

    //Jump
    [Header("Ground Check")]
    [SerializeField] private float _playerHeight;
    [SerializeField] private LayerMask _whatIsGround;
    private bool _isGrounded = false;

    [Header("Jump")]
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _jumpCooldown;
    [SerializeField] private float _airMultiplier;
    private bool _readyToJump = true;
    private float _jumpCounter = 0f;
    private bool _isFlying = false;
    private bool _spacePressed = false;
    private bool _shiftPressed = false;
    [SerializeField] private float _flyForce = 10f;


    //Look
    [Header("Look")]
    [SerializeField] private GameObject _cam;
    [SerializeField] private float _mouseSensitivity = 25f;
    private Vector2 _mouseDelta;
    private bool _isRotating = false;

    private void Awake()
    {
        _inputs = new IA_PlayerController();

        _rb = GetComponent<Rigidbody>();
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {

    }

    private void OnEnable()
    {
        _inputs.Enable();

        //Movement
        _inputs.Player.Move.performed += OnMovementPerformed;
        _inputs.Player.Move.canceled += OnMovementCancelled;

        //Jump
        _inputs.Player.Jump.performed += OnJumpPerformed;
        _inputs.Player.Jump.canceled += OnJumpCancelled;
        _inputs.Player.FlyDown.performed += OnFlyDownPerformed;
        _inputs.Player.FlyDown.canceled += OnFlyDownCancelled;

        //Look
        _inputs.Player.Look.performed += OnLookPerformed;
        _inputs.Player.Look.canceled += OnLookCancelled;
    }
    private void OnDisable()
    {
        _inputs.Disable();

        //Movement
        _inputs.Player.Move.performed -= OnMovementPerformed;
        _inputs.Player.Move.canceled -= OnMovementCancelled;

        //Jump
        _inputs.Player.Jump.performed -= OnJumpPerformed;
        _inputs.Player.Jump.canceled -= OnJumpCancelled;
        _inputs.Player.FlyDown.performed -= OnFlyDownPerformed;
        _inputs.Player.FlyDown.canceled -= OnFlyDownCancelled;

        //Look
        _inputs.Player.Look.performed -= OnLookPerformed;
        _inputs.Player.Look.canceled -= OnLookCancelled;
    }

    private void FixedUpdate()
    {
        PlayerMove();
        PlayerMouseLook();
        SpaceKeyAction();
        ShiftKeyAction();
    }

    private void Update()
    {
        // Ground check
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _whatIsGround);

        // Handle drag 
        if (_isGrounded)
        {
            _rb.drag = _groundDrag;
            _readyToJump = true;
            _isFlying = false;
        }
        else
        {
            _rb.drag = 0f;
        }

        // Flying
        if (_isFlying)
        {
            _rb.useGravity = false;
        }
        else
        {
            _rb.useGravity = true;
        }
    }

    // Movement
    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        _moveDir = value.ReadValue<Vector2>();
    }

    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        _moveDir = Vector2.zero;
    }

    private void PlayerMove()
    {
        Vector3 forward = _cam.transform.forward;
        forward.y = 0f;

        Vector3 movement = forward.normalized * _moveDir.y + _cam.transform.right.normalized * _moveDir.x;
        movement.y = 0f;

        movement *= _speed * Time.fixedDeltaTime;

        // Déplacement du joueur
        _rb.MovePosition(_rb.position + movement);
    }

    // Jump
    private void OnJumpPerformed(InputAction.CallbackContext value)
    {
        _spacePressed = IsSpaceKeyHeld();

        DoubleJumpCheck(); 
    }

    private void OnJumpCancelled(InputAction.CallbackContext value)
    {
        _spacePressed = IsSpaceKeyHeld();

        if (_isFlying)
        {
            StopMovement();
        }
    }
    private void OnFlyDownPerformed(InputAction.CallbackContext value)
    {
        _shiftPressed = IsShiftKeyHeld();
    }

    private void OnFlyDownCancelled(InputAction.CallbackContext value)
    {
        _shiftPressed = IsShiftKeyHeld();

        if (_isFlying)
        {
            StopMovement();
        }
    }

    private void SpaceKeyAction()
    {
        if (_spacePressed)
        {
            if (!_isFlying)
            {
                if (_readyToJump && _isGrounded)
                {
                    ImpulsePlayer();
                    _readyToJump = false;
                }
            }
            else
            {
                FlyHigher();
            }
        }
    }

    private void ShiftKeyAction()
    {
        if (_shiftPressed)
        {
            if (_isFlying)
            {
                FlyDown();
            }
        }
    }

    private void StopMovement()
    {
        _rb.velocity = Vector3.zero;
    }

    private bool IsSpaceKeyHeld()
    {
        return _inputs.Player.Jump.ReadValue<float>() > 0.1f;
    }

    private bool IsShiftKeyHeld()
    {
        return _inputs.Player.FlyDown.ReadValue<float>() > 0.1f;
    }

    private void DoubleJumpCheck()
    {
        _jumpCounter++;

        if (_jumpCounter >= 2)
        {
            _isFlying = !_isFlying;
        }

        Invoke("ResetJumpCounter", 0.4f);
    }
    private void ImpulsePlayer()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

    private void FlyHigher()
    {
        _rb.AddForce(Vector3.up * _flyForce, ForceMode.Force);
    }

    private void FlyDown()
    {
        _rb.AddForce(Vector3.down * _flyForce, ForceMode.Force);
    }

    private void ResetJumpCounter()
    {
        _jumpCounter = 0f;
    }

    //Look
    private void OnLookPerformed(InputAction.CallbackContext value)
    {
        _mouseDelta = value.ReadValue<Vector2>();
        _isRotating = true;
    }

    private void OnLookCancelled(InputAction.CallbackContext value)
    {
        _mouseDelta = Vector2.zero;
        _isRotating = false;
    }

    private void PlayerMouseLook()
    {
        if (!_isRotating)
        {
            if (Mathf.Abs(_rb.angularVelocity.magnitude) > 0f)
            {
                _rb.angularVelocity = Vector3.zero;
            }
            return;
        }

        float mouseX = _mouseDelta.x * _mouseSensitivity * Time.deltaTime;
        float mouseY = _mouseDelta.y * _mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        float desiredRotationX = _cam.transform.eulerAngles.x - mouseY;

        if (desiredRotationX > 180)
        {
            desiredRotationX -= 360;
        }
        desiredRotationX = Mathf.Clamp(desiredRotationX, -90f, 90f);

        _cam.transform.eulerAngles = new Vector3(desiredRotationX, _cam.transform.eulerAngles.y, 0);
    }

}
