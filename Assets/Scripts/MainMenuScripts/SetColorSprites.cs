using System.Collections.Generic;
using UnityEngine;

public class SetColorSprites : MonoBehaviour {
    [SerializeField] private Sprite red;
    [SerializeField] private Sprite blue;
    [SerializeField] private Sprite green;
    [SerializeField] private Sprite orange;
    [SerializeField] private Sprite yellow;
    [SerializeField] private Sprite white;
    [SerializeField] private Sprite black;
    
    
    // Sprite Mapping
    private Dictionary<string, Sprite> spriteMapping;
    
    private void Awake() {
        // create mapping
        spriteMapping = new Dictionary<string, Sprite>()  {
            {"red", red},
            {"blue", blue},
            {"green", green},
            {"orange", orange},
            {"yellow", yellow},
            {"white", white},
            {"black", black}
        };

        TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer);
        if(spriteRenderer!=null) {
            // get color and map color
            string color = PlayerPrefs.GetString("characterColor", "red");
            spriteMapping.TryGetValue(color, out Sprite currSprite);
            
            // set the sprite with appropriate color
            if(currSprite!=null) spriteRenderer.sprite = currSprite;
            else spriteRenderer.sprite = red;

        } // just skip this step if there is no Sprite Renderer found in this object
    }



    // call this again when avatar changes - becareful, change avatar first and then update
    public void updateAvatarSprite() {
        TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer);
        if(spriteRenderer!=null) {
            // get color and map color
            string color = PlayerPrefs.GetString("characterColor", "red");
            spriteMapping.TryGetValue(color, out Sprite currSprite);
            
            // set the sprite with appropriate color
            if(currSprite!=null) spriteRenderer.sprite = currSprite;
            else spriteRenderer.sprite = red;

        } // just skip this step if there is no Sprite Renderer found in this object
    }
}
