using Unity.VisualScripting;
using UnityEngine;

public class EnemyAnimatorControl : MonoBehaviour {
    private Animator animator;
    [SerializeField] private EnemyModeController enemyModeController;
    [SerializeField] private EnemyNormalLogic enemyNormalLogic;
    [SerializeField] private EnemyHostileLogic enemyHostileLogic;
    
    private void OnEnable() {
        animator = GetComponent<Animator>();
        if(enemyNormalLogic==null) Destroy(this.gameObject);
    }
    private void Update() {
        if(enemyNormalLogic.getIsMoving()) {
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