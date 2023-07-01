using Unity.VisualScripting;
using UnityEngine;

public class EnemyHostileLogic : MonoBehaviour {
    [SerializeField] private GameObject enemy;
    private GameObject player;
    [SerializeField] private float damageRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private EnemyStats enemyStats;
    [SerializeField] private float timeBetweenDamage;
    [SerializeField] private float AtkForce;
    [SerializeField] private float forceDirx;
    [SerializeField] private float forceDiry;
    private bool inDamageRange;
    private float lastTimeDamaged;
    private Vector3 moveDir;
    private Rigidbody2D enemyRB;
    private void OnEnable() {
        player = GameObject.FindGameObjectWithTag("Player");
        moveDir = new Vector3(1, 0, 0); // only one dimensional movements allowed
        enemyRB = GetComponentInParent<Rigidbody2D>();
        lastTimeDamaged=-1*timeBetweenDamage;
    }

    private void Update() {
        if(player.IsDestroyed()) {
            this.gameObject.SetActive(false);
            return;
        }
        getIfplayerInDamageRange();
        if(!inDamageRange) {
            // move to enemy
            if(player.transform.position.x-enemy.transform.position.x>0) moveDir.x=1; // move forward dir
            else moveDir.x=-1; // move backward dir
            float moveDist = moveSpeed*Time.deltaTime;
            enemy.transform.position+=moveDir*moveDist;
        } else {
            // do damage
            if(Time.time-lastTimeDamaged>timeBetweenDamage) {
                enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(forceDirx*AtkForce, forceDiry*AtkForce));
                player.GetComponent<PlayerLogic>().gotHit(enemyStats.getAttackPower());
                lastTimeDamaged=Time.time;
            }
        }
    }

    private void getIfplayerInDamageRange() {
        if(Mathf.Abs(player.transform.position.x-enemy.transform.position.x)<=damageRange) inDamageRange=true;
        else inDamageRange=false;
    }
}
