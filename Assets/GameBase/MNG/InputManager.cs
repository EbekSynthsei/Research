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
        return inputActions.GamePlay.Jump.triggered;        
    }
    public bool OnCrouchPressed()
    {
        return inputActions.GamePlay.Crouch.triggered;
    }
    public bool OnPrimaryAttackPressed()
    {
        return inputActions.GamePlay.PrimaryAttack.triggered;
    }
    public bool OnSecondaryAttackPressed()
    {
        return inputActions.GamePlay.SecondaryAttack.triggered;
    }

    public bool OnDashPressed()
    {

        return inputActions.GamePlay.Dash.triggered;
    }
}
