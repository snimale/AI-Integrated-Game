using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Unity.VisualScripting;

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
    public event EventHandler OnTestKeyClick;
    public event EventHandler OnSpacePressed;
    
    // keeping track of skill cooldowns w.r.t one fixed value
    [SerializeField] private float skillCooldown;
    private float lastTimeAttacked;
    private float lastTimeGuarded;
    private float lastTimeJumped;

    private void Awake() {
        playerControls = new PlayerControls();
        isWalking=false;
        isIdle=false;
    }

    private void OnEnable() {
        walk = playerControls.Player.Walk;
        playerControls.Player.Attack.performed+=TriggerAttackEvent;
        playerControls.Player.Guard.performed+=TriggerGuardEvent;
        playerControls.Player.testInput.performed+=TriggerTestEvent;
        playerControls.Player.Jump.performed+=TriggerJumpEvent;
        walk.Enable();
        playerControls.Player.Attack.Enable();
        playerControls.Player.Guard.Enable();
        playerControls.Player.testInput.Enable();
        playerControls.Player.Jump.Enable();

        // set initial cooldown managing vars
        lastTimeAttacked=-1*skillCooldown;
        lastTimeGuarded=-1*skillCooldown;
        lastTimeJumped=-1*skillCooldown;
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
        playerControls.Player.Guard.Disable();
        playerControls.Player.testInput.Disable();
        playerControls.Player.Jump.Disable();
    }

    public Vector2 getMoveDir() {return moveDir;}
    public bool getIsWalking() {return isWalking;}
    public bool getIsIdle() {return isIdle;}
    private void TriggerAttackEvent(InputAction.CallbackContext context) {
        if(Time.time-lastTimeAttacked>skillCooldown) {
            OnLeftMouseClick?.Invoke(this, EventArgs.Empty);
            lastTimeAttacked=Time.time;
        }
    }

    private void TriggerGuardEvent(InputAction.CallbackContext context) {
        if(Time.time-lastTimeGuarded>skillCooldown) {
            OnRightMouseClick?.Invoke(this, EventArgs.Empty);
            lastTimeGuarded=Time.time;
        }
    }

    private void TriggerTestEvent(InputAction.CallbackContext context) {
        OnTestKeyClick?.Invoke(this, EventArgs.Empty);
    }

    private void TriggerJumpEvent(InputAction.CallbackContext context) {
        if(Time.time-lastTimeJumped>skillCooldown) {
            OnSpacePressed?.Invoke(this, EventArgs.Empty);
            lastTimeJumped=Time.time;
        }
        
    }
}
