using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerLogic : MonoBehaviour {
    [SerializeField] private PlayerInputs playerInputs;
    [SerializeField] private float moveDirIgnoreVal;
    [SerializeField] private float playerMovSpeed;
    [SerializeField] private PlayerHealthBar playerHealthBar;
    
    // 6 characters prefab
    [SerializeField] private GameObject knight;
    [SerializeField] private GameObject thief;
    [SerializeField] private GameObject soldier;
    [SerializeField] private GameObject merchant;
    [SerializeField] private GameObject peasant;
    [SerializeField] private GameObject priest;

    private int[] stats;

    // hashmap to choose character of player choice
    private Dictionary<String, GameObject> charVisualMapping;
    private Dictionary<String, int[]> charStatsMapping;
    
    private void OnEnable() {
        charVisualMapping = new Dictionary<string, GameObject>() {
            {"knight", knight},
            {"thief", thief},
            {"soldier", soldier},
            {"merchant", merchant},
            {"peasant", peasant},
            {"priest", priest}
        };

        charStatsMapping = new Dictionary<string, int[]>() {
            // hp - atk - matk - def - mdef - pp - mp
            {"knight", new int[] {400, 5, 0}},
            {"thief", new int[] {150, 20, 0}},
            {"soldier", new int[] {300, 10, 0}},
            {"merchant", new int[] {150, 7, 7}},
            {"peasant", new int[] {200, 15, 0}},
            {"priest", new int[] {150, 0, 20}}
        };

        String characterName =  PlayerPrefs.GetString("character", "knight");
        charVisualMapping.TryGetValue(characterName, out GameObject character);
        if(character==null) character=knight;
        GameObject visual = Instantiate (character, new Vector3(transform.position.x,transform.position.y, transform.position.z) , Quaternion.identity, this.transform);
        visual.SetActive(true);
        charStatsMapping.TryGetValue(characterName, out stats);
        if(stats==null) stats = new int[] {400, 5, 0};

        playerHealthBar.setHealthBarMax(stats[0]);
    }

    private void Update() {
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

    // subscribe this function to an event that is triggered every time damage takes place
    private void doOnDamage() {
        stats[0]-=50; // opponents attack 
    }

    public int[] getPlayerStats() {
        return stats;
    }
}
