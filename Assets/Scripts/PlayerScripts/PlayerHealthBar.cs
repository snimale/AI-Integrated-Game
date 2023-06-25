using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour {
    [SerializeField] private Slider bar;
    [SerializeField] private PlayerInputs playerInputs;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private float lerpDuration;
    private void OnEnable() {
        setHealthBarMax(playerStats.getHealth());
        // have to subscribe to damage input
    }

    private void setHealthBar_OnTestKeyClick(object sender, EventArgs e) {
        int currHealth = playerStats.getHealth();
        bar.value=currHealth;
    }

    private void setHealthBarMax(int maxHealth) {
        bar.maxValue = maxHealth;
        bar.value = maxHealth;
    }
}
