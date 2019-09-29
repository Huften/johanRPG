using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    [SerializeField]
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public float attackRange;
    public float timeAnimationHit = 0;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public Animator playerAnim;
    Rigidbody2D rb2d;

    [HideInInspector]
    public bool attacking;
    public float attackDamage;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
   void Update()
    {
        if(timeBtwAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Vector3 theScale = transform.localScale;

                attacking = true;
                playerAnim.SetTrigger("attack");
                rb2d.velocity = Vector2.zero; //Works, but slow

               // _ = rb2d.GetComponent<CharacterController>().facingRight == true ? theScale.x = 1 : theScale.x = -1;
             //    if (rb2d.GetComponent<CharacterController>().facingRight == true)
             //    {                   
             //        theScale.x = 1;
             //    }
             //    else
             //    {
             //        theScale.x = -1;
             //    }


                StartCoroutine(DamageDelay(timeAnimationHit));

                timeBtwAttack = startTimeBtwAttack;
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }
    IEnumerator DamageDelay(float time)
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
        print("waiting for damage");
        yield return new WaitForSeconds(time);

        for (int j = 0; j < enemiesToDamage.Length; j++)
        {
            enemiesToDamage[j].GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }
        attacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
