using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponBehavior : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask playerLayers;

    private float attackRange = 0.5f;
    private int attackDamage = 10;

    void AttackPlayer()
    {
        // Detect enemy in range of attack
        Collider[] hitPlayer = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayers);

        // Damage enemy
        foreach (Collider player in hitPlayer)
        {
            player.GetComponent<PlayerController>().TakeDamage(attackDamage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            AttackPlayer();
        }
    }
}
