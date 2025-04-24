using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private Vector2 _movementInput;
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();
        Debug.Log(_movementInput);
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Jump button pushed down now");
        }
        if (context.performed)
        {
            Debug.Log("Jump button is being held down");
        }
        if (context.canceled)
        {
            Debug.Log("Jump button has been released");
        }
    }
}
