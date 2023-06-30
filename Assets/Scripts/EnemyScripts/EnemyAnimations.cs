using Unity.VisualScripting;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour {
    private Animator animator;
    private EnemyLogic enemyLogic;
    
    private void OnEnable() {
        animator = GetComponentInChildren<Animator>();
        enemyLogic = GetComponent<EnemyLogic>();
        if(enemyLogic==null) Destroy(this.gameObject);
    }
    private void Update() {
        if(enemyLogic.getIsMoving()) {
            animator.SetBool("moving", true);
        } else {
            animator.SetBool("moving", false);
        }
    }

    private void jump_OnJumpEvent() {
        
    }
    private void attack_OnEnemyContact() {

    }
}