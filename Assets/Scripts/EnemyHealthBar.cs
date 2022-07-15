using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    private Image healthBar;
    private EnemyBehavior enemyBehaviorScript;

    private float currentHealth;
    private float maxHealth = 100f;

    void Start()
    {
        healthBar = GetComponent<Image>();
        enemyBehaviorScript = FindObjectOfType<EnemyBehavior>();
    }

    void Update()
    {
        currentHealth = enemyBehaviorScript.currentEnemyHealth;
        healthBar.fillAmount = currentHealth / maxHealth;
    }
}
