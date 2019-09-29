using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    Animator anim;
    PlayerHealth playerHealth;
    GameObject PlayerHealth;
    GameObject player;

    public float attackDamage = 0.2f;
    public float timeBetweenAttacks = 1;

    bool playerInRange;
    bool inCoroutine;
    float timer;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
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
            //anim.SetBool("playerInRange", false);
            playerInRange = false;
        }
    }
    void Update()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
        if (timer >= timeBetweenAttacks && playerInRange == true)
        {
            //anim.SetBool("playerInRange", true);
            // ... attack.
            Damage();

        }
        else
        {

        }

        // If the player has zero or less health...
        if (playerHealth.currentHealth <= 0)
        {
            // ... tell the animator the player is dead.
            //anim.SetTrigger("PlayerDead");
            //anim.SetBool("playerInRange", false);
        }
    }


    void Damage()
    {
        // Reset the timer.
        timer = 0f;
        //anim.SetBool("walkToPlayer", false);
        // If the player has health to lose...
        if (playerHealth.currentHealth > 0)
        {
            // ... damage the player
            playerHealth.TakeDamage(attackDamage);
            print("hitByFireball");
        }
    }

    IEnumerator DamageDelay(float time)
    {
        inCoroutine = false;
        yield return new WaitForSeconds(time);

        print("hit");
        inCoroutine = true;
    }

}