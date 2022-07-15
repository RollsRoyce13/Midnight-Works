using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemyLayers;

    private float attackRange = 0.5f;
    public int attackDamage = 10;

    public void Attack()
    {
        // Detect enemy in range of attack
        Collider[] hitEnemy = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        // Damage enemy
        foreach (Collider enemy in hitEnemy)
        {
            enemy.GetComponent<EnemyBehavior>().TakeDamage(attackDamage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Enemy")
        {
            Attack();
        }
    }
}
