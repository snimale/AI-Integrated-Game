using System;
using UnityEngine;


public class PlayerAnimatorControl : MonoBehaviour {
    private Animator animator;
    private PlayerInputs playerInputs;
    private Boolean lookingRight;

    private void Start() {
        animator = GetComponent<Animator>();
        playerInputs = GameObject.FindGameObjectWithTag("InputManager").GetComponent<PlayerInputs>();
        playerInputs.OnLeftMouseClick+=animateAttack_OnLeftMouseClick;
        playerInputs.OnRightMouseClick+=animateGuard_OnRightMouseClick;
        GetComponentInParent<PlayerLogic>().OnPlayerDeath+=animateDeath_OnPlayerDeath;
        lookingRight=false;
    }

    private void Update() {
        animator.SetBool("isWalking", playerInputs.getIsWalking());
        animator.SetBool("isIdle", playerInputs.getIsIdle());
        if(playerInputs.getMoveDir().x<0 && lookingRight) {
            this.gameObject.transform.Rotate(0f, 180f, 0f, Space.Self);
            lookingRight=false;
        }
        if(!lookingRight && playerInputs.getMoveDir().x>0) {
            this.gameObject.transform.Rotate(0f, 180f, 0f, Space.Self);
            lookingRight=true;
        }
    }

    private void animateAttack_OnLeftMouseClick(object sender, EventArgs e) {
        animator.StopPlayback();
        animator.Play("attack");
    }

    private void animateGuard_OnRightMouseClick(object sender, EventArgs e) {
        animator.StopPlayback();
        animator.Play("guard");
    }

    private void animateDeath_OnPlayerDeath(object sender, EventArgs e) {
        animator.StopPlayback();
        animator.Play("die");
    }
}
