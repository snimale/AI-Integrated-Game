using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour {
    [SerializeField] private Slider bar;
    [SerializeField] private PlayerInputs playerInputs;
    [SerializeField] private PlayerLogic playerLogic;
    [SerializeField] private float lerpDuration;

    private void setHealthBar_OnTestKeyClick(object sender, EventArgs e) {
        int[] playerStats = playerLogic.getPlayerStats();
        bar.value=playerStats[0];
    }

    public void setHealthBarMax(int maxHealth) {
        bar.maxValue = maxHealth;
        bar.value = maxHealth;
    }
}
