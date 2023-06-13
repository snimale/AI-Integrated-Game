using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerLogic : MonoBehaviour {
    [SerializeField] private PlayerInputs playerInputs;
    [SerializeField] private float moveDirIgnoreVal;
    [SerializeField] private float playerMovSpeed;
    
    // 6 characters prefab
    [SerializeField] private GameObject knight;
    [SerializeField] private GameObject thief;
    [SerializeField] private GameObject soldier;
    [SerializeField] private GameObject merchant;
    [SerializeField] private GameObject peasant;
    [SerializeField] private GameObject priest;

    // hashmap to choose character of player choice
    private Dictionary<String, GameObject> charMapping;
    
    private void OnEnable() {
        charMapping = new Dictionary<string, GameObject>() {
            {"knight", knight},
            {"thief", thief},
            {"soldier", soldier},
            {"merchant", merchant},
            {"peasant", peasant},
            {"priest", priest}
        };
        String characterName =  PlayerPrefs.GetString("character", "knight");
        charMapping.TryGetValue(characterName, out GameObject character);
        if(character==null) character=knight;
        GameObject visual = Instantiate (character, new Vector3(transform.position.x,transform.position.y, transform.position.z) , Quaternion.identity, this.transform);
        visual.SetActive(true);
    }

    private void Update() {
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
}
