using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    //public GameObject projectile;
    public Rigidbody2D projectile;
    [HideInInspector]
    public Rigidbody2D clone;
    GameObject player;
    Animator anim;
    PlayerHealth playerHealth;
    // Start is called before the first frame update
    
    bool playerInRange;
    float timer;
    public float timeBetweenAttacks = 2.0f;
    public int fireballAmount = 10;
    public int timeBeforeDestroy = 5;
    public int[] fireballCount;



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

            Fire();

        }


        // If the player has zero or less health...
        if (playerHealth.currentHealth <= 0)
        {
            // ... tell the animator the player is dead.
            //anim.SetTrigger("PlayerDead");
            //anim.SetBool("playerInRange", false);
        }

    }

    void Fire()
    {
        // Reset the timer.
        timer = 0f;
        //anim.SetBool("walkToPlayer", false);
        // If the player has health to lose...
        if (playerHealth.currentHealth > 0)
        {

            //  switch (fireballAmount)
            //  {
            //      case int n when (n >= 1):
            //          clone.velocity = transform.TransformDirection(0, -1, 0 * 10);
            //          break;
            //      case int n when (n >= 2):
            //          clone.velocity = transform.TransformDirection(1, -1, 0 * 10);
            //          break;
            //      case int n when (n >= 3):
            //          clone.velocity = transform.TransformDirection(-1, -1, 0 * 10);
            //          break;
            //  }


            if (fireballAmount >= 1)
            {
                clone = Instantiate(projectile, transform.position, transform.rotation);
                clone.velocity = transform.TransformDirection(0, -1, 0 * 10);

            }
            if(fireballAmount >= 2)
            {
                clone = Instantiate(projectile, transform.position, transform.rotation);
                clone.velocity = transform.TransformDirection(1, -1, 0 * 10);

            }
            if(fireballAmount >= 3)
            {
                clone = Instantiate(projectile, transform.position, transform.rotation);
                clone.velocity = transform.TransformDirection(-1, -1, 0 * 10);

            }
            // ... damage the player.          
        }

    }


}

