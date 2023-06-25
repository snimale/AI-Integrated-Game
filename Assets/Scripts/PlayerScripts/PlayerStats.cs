using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    private int health;
    private int mp;
    private int atk;
    private int matk;
    private int def;
    private int mdef;
    readonly private Dictionary<string, int[]> charStatsMapping = new Dictionary<string, int[]> {
        // hp - mp - atk - matk - def - mdef
        {"knight", new int[] {400, 50, 15, 0, 20, 15}},
        {"thief", new int[] {150, 150, 30, 10, 5, 5}},
        {"soldier", new int[] {300, 100, 20, 0, 25, 10}},
        {"merchant", new int[] {150, 100, 10, 10, 5, 5}},
        {"peasant", new int[] {200, 100, 20, 5, 10, 10}},
        {"priest", new int[] {150, 250, 0, 35, 5, 5}}
    };

    private void Awake() {
        setStats(getStats());
    }

    private void setStats(int[] stats) {
        health=stats[0];
        mp=stats[1];
        atk=stats[2];
        matk=stats[3];
        def=stats[4];
        mdef=stats[5];
    }

    private int[] getStats() {
        charStatsMapping.TryGetValue(PlayerPrefs.GetString("character"), out int[] stats);
        if(stats==null) stats = new int[] {400, 50, 15, 0, 20, 15}; // defalut
        return stats;
    }

    public int getHealth() {
        if(health==0) setStats(getStats());
        return health;
    }
}
