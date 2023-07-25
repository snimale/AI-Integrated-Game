using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsUtil : MonoBehaviour {
    // player hp low -> higher attack slime spawn
    // player mp low -> higher physical defence slime spawn
    // player mp high -> higher magical defence slime spawn
    // higher player level -> higher slime stats
    // higher slime kill count -> higher slime stats
    // slime stats can varry depending on the weapon held
    // slime varation based on zones

    private Dictionary<int, int[]> slimeData= new Dictionary<int, int[]>() {
        {1, new int[] {1, 2, 3}}
    };
    
    public int getAttackPower() {
        return 50;
    }
}
