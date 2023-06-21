using System.Collections.Generic;
using UnityEngine;

public class CustumizeCharacterButtons : MonoBehaviour {
    private Dictionary<int, string> currJobMapping;
    private Dictionary<int, string> currColorMapping;
    private int currColorIndex;
    private int currJobIndex;
    private void Awake() {
        // map the jobs and colors
        currColorMapping = new Dictionary<int, string>() {
            {0, "red"},
            {1, "blue"},
            {2, "green"},
            {3, "orange"},
            {4, "yellow"},
            {5, "white"},
            {6, "black"}
        };

        currJobMapping = new Dictionary<int, string>() {
            {0, "knight"},
            {1, "priest"},
            {2, "peasant"},
            {3, "merchant"},
            {4, "soldier"},
            {5, "thief"}
        };


        currJobIndex = PlayerPrefs.GetInt("characterIndex", 0);
        currJobMapping.TryGetValue(currJobIndex, out string currJob);
        if(currJob!=null) PlayerPrefs.SetString("character", currJob);

        currColorIndex = PlayerPrefs.GetInt("colorIndex", 0);
        currColorMapping.TryGetValue(currColorIndex, out string currColor);
        if(currColor!=null) PlayerPrefs.SetString("characterColor", currColor);
    }

    public void updateCharacterColor() {
        currColorIndex=(currColorIndex+1)%7; // 7 is number of colors
        PlayerPrefs.SetInt("colorIndex", currColorIndex);
        currColorMapping.TryGetValue(currColorIndex, out string currColor);
        if(currColor!=null) PlayerPrefs.SetString("characterColor", currColor);
    }

    public void updateCharacterJob() {
        currJobIndex=(currJobIndex+1)%6; // 7 is number of colors
        PlayerPrefs.SetInt("characterIndex", currJobIndex);
        currJobMapping.TryGetValue(currJobIndex, out string currJob);
        if(currJob!=null) PlayerPrefs.SetString("character", currJob);
    }
}
