using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField]
    private float _inputHoldTime = 0.2f;

    private PlayerInput _playerInput;
    private Camera _camera;

    public Vector2 RawMovementInput { get; private set; }
    public Vector2 RawDashDirectionInput { get; private set; }
    public Vector2Int DashDirectionInput { get; private set; }
    public int NormalizeInputX { get; private set; }
    public int NormalizeInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool GrabInput { get; private set; }
    public bool DashInput { get; private set; }
    public bool DashInputStop { get; private set; }


    private float _jumpInputStartTime;
    private float _dashInputStartTime;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _camera = Camera.main;
    }
    private void Update()
    {
        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        if(Mathf.Abs(RawMovementInput.x) > 0.5f)
        {
            NormalizeInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        }
        else
        {
            NormalizeInputX = 0;
        }

        if(Mathf.Abs(RawMovementInput.y) > 0.5f)
        {
            NormalizeInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
        }
        else
        {
            NormalizeInputY = 0;
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            JumpInputStop = false;
            _jumpInputStartTime = Time.time;
        }

        if (context.canceled)
        {
            JumpInputStop = true;
        }
    }

    public void OnGrabInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GrabInput = true;
        }

        if (context.canceled)
        {
            GrabInput = false;
        }
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DashInput = true;
            DashInputStop = false;
            _dashInputStartTime = Time.time;
        }
        else if (context.canceled)
        {
            DashInputStop = true;
        }
    }

    public void OnDashDirectionInput(InputAction.CallbackContext context)
    {
        RawDashDirectionInput = context.ReadValue<Vector2>();

        if(_playerInput.currentControlScheme == "Keyboard")
        {
            RawDashDirectionInput = _camera.ScreenToWorldPoint((Vector3)RawDashDirectionInput) - transform.position;
        }

        DashDirectionInput = Vector2Int.RoundToInt(RawDashDirectionInput.normalized);
    }

    public void ChangeJumpInput() => JumpInput = false;

    public void ChangeDashInput() => DashInput = false;

    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= _jumpInputStartTime + _inputHoldTime)
        {
            JumpInput = false;
        }
    }

    private void CheckDashInputHoldTime()
    {
        if(Time.time >= _dashInputStartTime + _inputHoldTime)
        {
            DashInput = false;
        }
    }
}
