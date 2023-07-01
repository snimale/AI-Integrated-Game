using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLogic : MonoBehaviour {
    [SerializeField] private PlayerInputs playerInputs;
    [SerializeField] private float moveDirIgnoreVal;
    [SerializeField] private float playerMovSpeed;
    [SerializeField] private float jumpValue;
    [SerializeField] private PlayerHealthBar playerHealthBar;
    [SerializeField] private float deathAnimationTime;
    
    // 6 characters prefab
    [SerializeField] private GameObject knight;
    [SerializeField] private GameObject thief;
    [SerializeField] private GameObject soldier;
    [SerializeField] private GameObject merchant;
    [SerializeField] private GameObject peasant;
    [SerializeField] private GameObject priest;

    // hashmap to choose character of player choice
    private Dictionary<String, GameObject> charVisualMapping;
    public EventHandler OnPlayerDeath;
    private bool died=false;
    private float diedTime;
    private void OnEnable() {
        charVisualMapping = new Dictionary<string, GameObject>() {
            {"knight", knight},
            {"thief", thief},
            {"soldier", soldier},
            {"merchant", merchant},
            {"peasant", peasant},
            {"priest", priest}
        };

        // get character player had set, instantiate it inside this object and set it active
        String characterName =  PlayerPrefs.GetString("character", "knight");
        charVisualMapping.TryGetValue(characterName, out GameObject character);
        if(character==null) character=knight;
        GameObject visual = Instantiate (character, new Vector3(transform.position.x,transform.position.y, transform.position.z) , Quaternion.identity, this.transform);
        visual.SetActive(true);

        // subscribe to jump event
        playerInputs.OnSpacePressed+=jump_OnSpacePressed;
    }

    private void Update() {
        if(died && Time.time-diedTime>deathAnimationTime) {

            // and do whatever menu u want here
            // foreach(Transform child in transform) {
            //     Destroy(child.gameObject);
            // }
            Destroy(this.gameObject);
        
        }
        playerMovement();
    }

    private void playerMovement() {
        Vector2 moveDir = playerInputs.getMoveDir();
        if(moveDir.x<-moveDirIgnoreVal) {
            Vector3 moveDist = new Vector3(-1, 0, 0);
            moveDist*= playerMovSpeed * Time.deltaTime;
            this.gameObject.transform.position+=moveDist;
        } else if(moveDir.x>moveDirIgnoreVal) {
            Vector3 moveDist = new Vector3(1, 0, 0);
            moveDist*= playerMovSpeed * Time.deltaTime;
            this.gameObject.transform.position+=moveDist;
        }
    }

    private void jump_OnSpacePressed(object sender, EventArgs e) {
        this.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigidbody);
        rigidbody.AddForce(new Vector2(0, jumpValue));
    }

    public void gotHit(int damageTaken) {
        if(GetComponent<PlayerStats>().getHealth()-damageTaken<=0) {
            OnPlayerDeath?.Invoke(this, EventArgs.Empty);
            playerHealthBar.disableBar();
            diedTime = Time.time;
            died=true;
        }
        this.GetComponent<PlayerStats>().takeDamage(damageTaken);
        playerHealthBar.updateHealth();
    }
}
