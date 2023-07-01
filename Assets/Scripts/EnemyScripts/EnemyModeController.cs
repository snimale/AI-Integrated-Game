using Unity.VisualScripting;
using UnityEngine;

public class EnemyModeController : MonoBehaviour {
    private GameObject player;
    [SerializeField] private GameObject HostileModeObj;
    [SerializeField] private GameObject NormalModeObj;
    [SerializeField] private float MaxDetectionDist;
    [SerializeField] private float MaxDetectionHeight;
    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnEnable() {
        HostileModeObj.SetActive(false);
        NormalModeObj.SetActive(true);
    }

    private void Update() {
        if (Mathf.Abs(this.transform.position.x-player.transform.position.x)<MaxDetectionDist
           && (this.transform.position.y-player.transform.position.x)<MaxDetectionHeight) {
            HostileModeObj.SetActive(true);
            NormalModeObj.SetActive(false);
            GetComponent<EnemyModeController>().enabled = false;
        }
    }
}
