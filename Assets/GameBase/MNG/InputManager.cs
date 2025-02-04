using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Manages player input actions.
/// </summary>
public class InputManager : Singleton<InputManager>
{
    public PlayerActionControls inputActions;

    [SerializeField]

    public void Awake()
    {
        Debug.Log("Awake InputManager");
        inputActions = new PlayerActionControls();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    /// <summary>
    /// Gets the movement input vector.
    /// </summary>
    /// <returns>Movement input vector.</returns>
    public Vector2 OnMoveInput()
    {
        return inputActions.GamePlay.Move.ReadValue<Vector2>();
    }

    /// <summary>
    /// Gets the normalized horizontal input.
    /// </summary>
    /// <returns>Normalized horizontal input.</returns>
    public int NormInputX()
    {
        return (int)(OnMoveInput() * Vector2.right).normalized.x;
    }

    /// <summary>
    /// Gets the normalized vertical input.
    /// </summary>
    /// <returns>Normalized vertical input.</returns>
    public int NormInputY()
    {
        return (int)(OnMoveInput() * Vector2.up).normalized.y;
    }

    /// <summary>
    /// Checks if the grab input is pressed.
    /// </summary>
    /// <returns>True if grab input is pressed, otherwise false.</returns>
    public bool GrabInput()
    {
        var button = inputActions.GamePlay.Grab.ReadValue<float>();
        return button != 0;
    }

    /// <summary>
    /// Gets the mouse look input vector.
    /// </summary>
    /// <returns>Mouse look input vector.</returns>
    public Vector2 GetMouseLook()
    {
        return inputActions.GamePlay.Move.ReadValue<Vector2>();
    }

    /// <summary>
    /// Checks if the jump input is pressed.
    /// </summary>
    /// <returns>True if jump input is pressed, otherwise false.</returns>
    public bool OnJumpPressed()
    {
        return inputActions.GamePlay.Jump.triggered;
    }

    /// <summary>
    /// Checks if the crouch input is pressed.
    /// </summary>
    /// <returns>True if crouch input is pressed, otherwise false.</returns>
    public bool OnCrouchPressed()
    {
        return inputActions.GamePlay.Crouch.triggered;
    }

    /// <summary>
    /// Checks if the primary attack input is pressed.
    /// </summary>
    /// <returns>True if primary attack input is pressed, otherwise false.</returns>
    public bool OnPrimaryAttackPressed()
    {
        return inputActions.GamePlay.PrimaryAttack.triggered;
    }

    /// <summary>
    /// Checks if the secondary attack input is pressed.
    /// </summary>
    /// <returns>True if secondary attack input is pressed, otherwise false.</returns>
    public bool OnSecondaryAttackPressed()
    {
        return inputActions.GamePlay.SecondaryAttack.triggered;
    }

    /// <summary>
    /// Checks if the dash input is pressed.
    /// </summary>
    /// <returns>True if dash input is pressed, otherwise false.</returns>
    public bool OnDashPressed()
    {
        return inputActions.GamePlay.Dash.triggered;
    }
}
