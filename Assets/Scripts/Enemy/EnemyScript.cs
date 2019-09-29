using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{

    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    EnemyAttack enemyAttack;
    Animator anim;
    Rigidbody2D rb2d;
    public float Speed = 1.0f;
    Vector3 tempVect;

    private bool facingRight = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = new PlayerHealth();
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        enemyAttack = GetComponent<EnemyAttack>();
        rb2d = GetComponent<Rigidbody2D>();

        
    }

    // Update is called once per frame
    void Update()
    {
       Vector3 distance = transform.position - player.position;


        if (!enemyHealth.isDead)
        {
            if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
            {
                if (distance.magnitude < 2 && enemyAttack.playerInRange == false)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, Speed * Time.deltaTime);
                    enemyAttack.walkToPlayer = true;
                }
                else
                {
                    enemyAttack.walkToPlayer = false;
                    this.rb2d.velocity = new Vector2(0, 0);
                }
            }

            Flip();
        }
        else
        {
            return;
        }
    }

    //  void Flip()
    //  {
    //      if(player.position.x > transform.position.x)
    //      {
    //          //face right
    //          transform.localScale = new Vector3(1, 1, 1);
    //      }
    //      else if(player.position.x < transform.position.x)
    //      {
    //          //face left
    //          transform.localScale = new Vector3(-1, 1, 1);
    //      }
    //  }

    void Flip()
    {
        if (player.position.x > transform.position.x && !facingRight || player.position.x < transform.position.x && facingRight)
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;

            theScale.x *= -1;

            transform.localScale = theScale;
        }
    }


}
