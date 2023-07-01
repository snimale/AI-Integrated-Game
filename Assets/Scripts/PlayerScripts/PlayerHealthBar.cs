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

    public void updateHealth() {
        int currHealth = playerStats.getHealth();
        bar.value=currHealth;
    }

    private void setHealthBarMax(int maxHealth) {
        bar.maxValue = maxHealth;
        bar.value = maxHealth;
    }

    public void disableBar() {
        bar.gameObject.SetActive(false);
    }
}
