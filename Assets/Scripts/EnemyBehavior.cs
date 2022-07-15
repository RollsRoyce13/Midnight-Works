using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Animator enemyAnim;
    private Rigidbody enemyRb;
    private Transform player;
    private GameManager gameManager;

    private float movementSpeed = 2.0f;

    private bool isPlayerInZone;

    private int maxEnemyHealth = 100;
    public int currentEnemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyRb = GetComponent<Rigidbody>();
        currentEnemyHealth = maxEnemyHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // If the enemy see the player - go to the player for attack
        if (isPlayerInZone)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;

            enemyRb.MovePosition(transform.position + direction * movementSpeed * Time.deltaTime);
            enemyAnim.SetFloat("walking_forward", 1.0f);
        }
    }
    // Take damage to enemy
    public void TakeDamage(int damage)
    {
        currentEnemyHealth -= damage;

        if(currentEnemyHealth <= 0)
        {
            enemyAnim.SetFloat("isAttack", 0.0f);
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");

        // Die animation
        enemyAnim.SetTrigger("isDead");

        // Disable the enemy
        this.enabled = false;

        // Show win screen with restart button
        gameManager.WinScreen();
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the enemy see the player
        if(other.name == "Player")
        {
            isPlayerInZone = true;
            Debug.Log("Player detected - attack!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Stop walking if the player not in enemy zone
            if (other.name == "Player")
            {
                isPlayerInZone = false;
                enemyAnim.SetFloat("walking_forward", 0.0f);
                Debug.Log("Player out of range, resume patrol");
            }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Attack the player with animation
            if (collision.gameObject.name == "Player")
            {
                enemyAnim.SetFloat("isAttack", 1.0f);
            }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Disable attack
        if (collision.gameObject.name == "Player")
        {
            enemyAnim.SetFloat("isAttack", 0.0f);
        }
    }
}
