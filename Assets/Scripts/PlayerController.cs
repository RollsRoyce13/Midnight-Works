using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator playerAnim;
    public Transform attackPoint;
    private Rigidbody playerRb;
    private Transform weaponContainer;
    private AudioSource playerAudio;
    private GameManager gameManager;
    private EnemyBehavior enemyBehaviorScript;

    public LayerMask enemyLayers;

    private float attackRange = 0.5f;
    private float attackRate = 2.0f;
    private float nextAttackTime = 0f;
    private float movementSpeed = 2.0f;
    private float rotationSpeed = 45.0f;

    private int maxPlayerHealth = 100;
    public int currentPlayerHealth;

    void Start()
    {
        weaponContainer = GameObject.Find("Weapon Container").GetComponent<Transform>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyBehaviorScript = GameObject.Find("Enemy").GetComponent<EnemyBehavior>();
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        currentPlayerHealth = maxPlayerHealth;
    }

    void FixedUpdate()
    {
        // Check if !gameOver for player controller
        if (!gameManager.gameOver)
        {
            // Store user input
            Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            float hMouseInput = Input.GetAxis("Mouse X");

            // Move Player in localDirection
            moveDirection = transform.TransformDirection(moveDirection);
            playerRb.MovePosition(transform.position + moveDirection * movementSpeed * Time.fixedDeltaTime);

            // Rotate camera and player with mouse
            transform.Rotate(Vector3.up, hMouseInput * rotationSpeed * Time.fixedDeltaTime);
            PlayerAnimations();
        }
    }

    void Update()
    {
        // Check if !gameOver for attack
        if (!gameManager.gameOver)
        {
            if (Time.time >= nextAttackTime)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    WeaponAnimations();

                    // Time to next attack
                    nextAttackTime = Time.time + 3f / attackRate;
                }
            }
            if (Input.GetKey(KeyCode.Mouse1))
            {
                playerAnim.SetTrigger("block");
            }
        }
    }

    // Take damage to player
    public void TakeDamage(int damage)
    {
        currentPlayerHealth -= damage;

        if (currentPlayerHealth <= 0)
        {
            PlayerDie();
        }
    }

    void PlayerDie()
    {
        Debug.Log("Player died!");

        // Disable the player and an enemy script with animation
        enemyBehaviorScript.enemyAnim.enabled = false;
        enemyBehaviorScript.enabled = false;
        this.enabled = false;

        // Die animation
        playerAnim.SetTrigger("isDead");

        // Show game over screen with restart button
        gameManager.GameOverScreen();
    }

    // Show Gizmo of damage radius
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    // Play sound and destroy coins when they are collect
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Coins"))
        {
            playerAudio.Play();
            gameManager.PickupCoins(1);
            Destroy(collision.gameObject);
        }
    }

    // Set the Player animation
    void PlayerAnimations()
    {
        if (Input.GetKey(KeyCode.W))
        {
            playerAnim.SetFloat("walking_forward", 1.0f);
        }
        else
        {
            playerAnim.SetFloat("walking_forward", 0.0f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            playerAnim.SetFloat("walking_back", 1.0f);
        }
        else
        {
            playerAnim.SetFloat("walking_back", 0.0f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerAnim.SetFloat("walking_left", 1.0f);
        }
        else
        {
            playerAnim.SetFloat("walking_left", 0.0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            playerAnim.SetFloat("walking_right", 1.0f);
        }
        else
        {
            playerAnim.SetFloat("walking_right", 0.0f);
        }
    }

    // Each item should have it's own fighting animation
    void WeaponAnimations()
    {
        if (weaponContainer.transform.GetChild(0).gameObject.activeSelf)
            {
                playerAnim.SetTrigger("attack_1");
            }
        if (weaponContainer.transform.GetChild(1).gameObject.activeSelf)
            {
                playerAnim.SetTrigger("attack_2");
            }
        if (weaponContainer.transform.GetChild(2).gameObject.activeSelf)
            {
                playerAnim.SetTrigger("attack_3");
            }
    }
}
