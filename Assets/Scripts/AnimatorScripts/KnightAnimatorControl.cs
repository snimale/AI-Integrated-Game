using System;
using UnityEngine;


public class KnightAnimatorControl : MonoBehaviour {
    [SerializeField] private Animator knightAnimator;
    [SerializeField] private PlayerInputs playerInputs;

    private void Start() {
        playerInputs.OnLeftMouseClick+=animateAttack_OnLeftMouseClick;
        playerInputs.OnRightMouseClick+=animateGuard_OnRightMouseClick;
    }

    private void Update() {
        knightAnimator.SetBool("isWalking", playerInputs.getIsWalking());
        knightAnimator.SetBool("isRunning", playerInputs.getIsRunning());
        knightAnimator.SetBool("isIdle", playerInputs.getIsIdle());
    }

    private void animateAttack_OnLeftMouseClick(object sender, EventArgs e) {
        knightAnimator.StopPlayback();
        knightAnimator.Play("attack");
    }

    private void animateGuard_OnRightMouseClick(object sender, EventArgs e) {
        knightAnimator.StopPlayback();
        knightAnimator.Play("guard");
    }
}
