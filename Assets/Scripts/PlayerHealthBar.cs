using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    private Image healthBar;
    private PlayerController playerControllerScript;

    private float currentHealth;
    private int maxHealth = 100;

    void Start()
    {
        healthBar = GetComponent<Image>();
        playerControllerScript = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        currentHealth = playerControllerScript.currentPlayerHealth;
        healthBar.fillAmount = currentHealth / maxHealth;
    }
}
