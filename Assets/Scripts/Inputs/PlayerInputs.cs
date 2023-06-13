using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerInputs : MonoBehaviour {
    private PlayerControls playerControls;
    private InputAction walk;
    private Vector2 moveDir;

    // Boolean Values
    private bool isWalking;
    private bool isIdle;

    // Events 
    public event EventHandler OnLeftMouseClick;
    public event EventHandler OnRightMouseClick;

    private void Awake() {
        playerControls = new PlayerControls();
        isWalking=false;
        isIdle=false;
    }

    private void OnEnable() {
        walk = playerControls.Player.Walk;
        playerControls.Player.Attack.started+=TriggerAttackEvent;
        playerControls.Player.Guard.started+=TriggerGuardEvent;
        walk.Enable();
        playerControls.Player.Attack.Enable();
        playerControls.Player.Guard.Enable();
    }

    private void FixedUpdate() {
        moveDir = walk.ReadValue<Vector2>();
        if(moveDir==Vector2.zero) {
            isWalking=false;
            isIdle=true;
        } else {
            isWalking=true;
            isIdle=false;
        }
    }

    private void OnDisable() {
        walk.Disable();
        playerControls.Player.Attack.Disable();
    }

    public Vector2 getMoveDir() {return moveDir;}
    public bool getIsWalking() {return isWalking;}
    public bool getIsIdle() {return isIdle;}
    private void TriggerAttackEvent(InputAction.CallbackContext context) {
        OnLeftMouseClick?.Invoke(this, EventArgs.Empty);
    }

    private void TriggerGuardEvent(InputAction.CallbackContext context) {
        OnRightMouseClick?.Invoke(this, EventArgs.Empty);
    }
}
