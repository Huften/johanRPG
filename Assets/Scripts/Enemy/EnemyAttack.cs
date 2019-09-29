using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 2.0f;     // The time in seconds between each attack.
    public float attackDamage = 1;               // The amount of health taken away per attack.
    public float timeAnimationHit = 0;


    Animator anim;                              // Reference to the animator component.
    GameObject player;                          // Reference to the player GameObject.
    PlayerHealth playerHealth;                  // Reference to the player's health.
    EnemyHealth enemyHealth;                    // Reference to this enemy's health.
    EnemyScript enemyScript;
    [HideInInspector]
    public bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
    [HideInInspector]
    public bool walkToPlayer;
    float timer;                                // Timer for counting up to the next attack.
    [HideInInspector]
    public bool attacking;


    void Start()
    {
        // Setting up the references.
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        enemyScript = GetComponent<EnemyScript>();
        anim = GetComponent<Animator>();

        playerInRange = false;
        attacking = false;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        // If the entering collider is the player...
        if (other.gameObject.tag == "Player")
        {
            print("close to Player");
            // ... the player is in range.
            playerInRange = true;
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        // If the exiting collider is the player...
        if (other.gameObject.tag == "Player")
        {
            // ... the player is no longer in range.
            anim.SetBool("playerInRange", false);
            playerInRange = false;
        }
    }


    void Update()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if (timer >= timeBetweenAttacks && playerInRange == true && enemyHealth.currentHealth > 0)
        {
            anim.SetBool("playerInRange", true);
            // ... attack.
            Attack();
            print("attack");

        }
        else
        {
            
            Walking();
        }

        // If the player has zero or less health...
        if (playerHealth.currentHealth <= 0)
        {
            // ... tell the animator the player is dead.
            anim.SetTrigger("PlayerDead");
            anim.SetBool("playerInRange", false);
        }
    }


    void Attack()
    {
        // Reset the timer.
        timer = 0f;
        anim.SetBool("walkToPlayer", false);
        // If the player has health to lose...
        if (playerHealth.currentHealth > 0)
        {
            print("hit");
            // ... damage the player.
            StartCoroutine(DamageDelay(timeAnimationHit));
        }
    }

    void Walking()
    {
        if (walkToPlayer == true)
        {
            anim.SetBool("walkToPlayer", true);
        }
        else
        {
            anim.SetBool("walkToPlayer", false);
        }
    }

    IEnumerator DamageDelay(float time)
    {
        yield return new WaitForSeconds(time);
        if(playerInRange == true)
        {
            playerHealth.TakeDamage(attackDamage);
        }

    }
}


