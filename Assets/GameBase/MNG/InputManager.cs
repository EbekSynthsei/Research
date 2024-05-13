using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 
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

    public Vector2 OnMoveInput()
    {
        Debug.Log("On Move Input");
        return inputActions.GamePlay.Move.ReadValue<Vector2>();
    }
    public int NormInputX()
    {   
        return (int)(OnMoveInput() * Vector2.right).normalized.x;
    }
    public int NormInputY()
    {
        return (int)(OnMoveInput() * Vector2.up).normalized.y;
    }

    public bool GrabInput()
    {
        Debug.Log("Grab!");
        var button = inputActions.GamePlay.Grab.ReadValue<float>();
        if (button != 0)
        {
            return true;
        }
        else return false;
    }
    public Vector2 GetMouseLook()
    {
        return inputActions.GamePlay.Move.ReadValue<Vector2>();
    }

    public bool OnJumpPressed()
    {
        Debug.Log("Jump!");
        return inputActions.GamePlay.Jump.triggered;        
    }
    public bool OnCrouchPressed()
    {
        Debug.Log("Crouch!");
        return inputActions.GamePlay.Crouch.triggered;
    }
    public bool OnPrimaryAttackPressed()
    {
        Debug.Log("Primary!");
        return inputActions.GamePlay.PrimaryAttack.triggered;
    }
    public bool OnSecondaryAttackPressed()
    {
        Debug.Log("Interaction!");
        return inputActions.GamePlay.SecondaryAttack.triggered;
    }
    public bool OnDashPressed()
    {
        Debug.Log("Dash!");
        return inputActions.GamePlay.Dash.triggered;
    }
}
