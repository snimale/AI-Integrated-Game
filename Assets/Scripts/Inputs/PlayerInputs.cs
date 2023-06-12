using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerInputs : MonoBehaviour {
    private PlayerControls playerControls;
    private InputAction walk;
    private InputAction run;

    // Boolean Values
    private bool isWalking;
    private bool isRunning;
    private bool isIdle;

    // Events 
    public event EventHandler OnLeftMouseClick;
    public event EventHandler OnRightMouseClick;

    private void Awake() {
        playerControls = new PlayerControls();
        isWalking=false;
        isRunning=false;
        isIdle=false;
    }

    private void OnEnable() {
        walk = playerControls.Player.Walk;
        run = playerControls.Player.Run;
        playerControls.Player.Attack.started+=TriggerAttackEvent;
        playerControls.Player.Guard.started+=TriggerGuardEvent;
        walk.Enable();
        run.Enable();
        playerControls.Player.Attack.Enable();
        playerControls.Player.Guard.Enable();
    }

    private void FixedUpdate() {
        if(!walk.IsPressed() && !run.IsPressed()) {
            isWalking=false;
            isRunning=false;
            isIdle=true;
        } else if(run.IsPressed()) {
            isRunning=true;
            isWalking=false;
            isIdle=false;
        } else {
            isWalking=true;
            isRunning=false;
            isIdle=false;
        }
    }

    private void OnDisable() {
        walk.Disable();
        run.Disable();
        playerControls.Player.Attack.Disable();
    }


    public bool getIsRunning() {return isRunning;}
    public bool getIsWalking() {return isWalking;}
    public bool getIsIdle() {return isIdle;}
    private void TriggerAttackEvent(InputAction.CallbackContext context) {
        OnLeftMouseClick?.Invoke(this, EventArgs.Empty);
    }

    private void TriggerGuardEvent(InputAction.CallbackContext context) {
        OnRightMouseClick?.Invoke(this, EventArgs.Empty);
    }
}
