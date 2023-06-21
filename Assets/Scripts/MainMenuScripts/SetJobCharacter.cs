using System.Collections.Generic;
using UnityEngine;

public class SetJobCharacter : MonoBehaviour {
    [SerializeField] private GameObject knight;
    [SerializeField] private GameObject priest;
    [SerializeField] private GameObject peasant;
    [SerializeField] private GameObject merchant;
    [SerializeField] private GameObject soldier;
    [SerializeField] private GameObject thief;

    // job mapping
    Dictionary<string, GameObject> jobMapping;
    private void Awake() {
        // first disable all the childern
        foreach (Transform child in transform) child.gameObject.SetActive(false);

        jobMapping = new Dictionary<string, GameObject>() {
            {"knight", knight},
            {"priest", priest},
            {"peasant", peasant},
            {"merchant", merchant},
            {"soldier", soldier},
            {"thief", thief}
        };

        string currJob = PlayerPrefs.GetString("character", "knight");
        jobMapping.TryGetValue(currJob, out GameObject currJobAvatar);
        if(currJobAvatar!=null) currJobAvatar.SetActive(true);
        else knight.SetActive(true);
    }



    public void updateJobCharacter() {
        foreach (Transform child in transform) child.gameObject.SetActive(false);
        string currJob = PlayerPrefs.GetString("character", "knight");
        jobMapping.TryGetValue(currJob, out GameObject currJobAvatar);
        if(currJobAvatar!=null) currJobAvatar.SetActive(true);
        else knight.SetActive(true);
    }
    

}