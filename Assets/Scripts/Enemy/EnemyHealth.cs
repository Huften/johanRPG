using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float startingHealth = 100;            
    public float currentHealth;                   
    public float sinkSpeed = 2.5f;             
    public int scoreValue = 10;                 
    public AudioClip deathClip;
    public Slider healthSlider;
    public GameObject hitEffect;


    Animator anim;                         
    AudioSource enemyAudio;                          
    new BoxCollider2D collider2D;
    [HideInInspector]
    public bool isDead;                                  
    


    void Start()
    {
        // Setting up the references.
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        collider2D = GetComponent<BoxCollider2D>();

        // Setting the current health when the enemy first spawns.
        currentHealth = startingHealth;
    }

    void Update()
    {
    }


    public void TakeDamage(float amount)
    {
        if (isDead)
            // ... no need to take damage so exit the function.
            return;

        // Play hit animation
        anim.SetTrigger("Hit");
        // Play the hurt sound effect.
        enemyAudio.Play();

        // Reduce the current health by the amount of damage sustained.
        currentHealth -= amount;
        print("enemy damage taken");

        Instantiate(hitEffect, transform.position, Quaternion.identity);

        // If the enemy is dead...
        if (currentHealth <= 0 && !isDead)
        {
            // ... the enemy is dead.S
            Death();
        }
        else
        {
            isDead = false;
        }

    }
   
    void Death()
    {
        // The enemy is dead.
        isDead = true;

        // Turn the collider into a trigger so shots can pass through it.
        collider2D.isTrigger = true;

        // Tell the animator that the enemy is dead.
        anim.SetTrigger("Dead");

        // Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
        enemyAudio.clip = deathClip;
        enemyAudio.Play();
    }


}


