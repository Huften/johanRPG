using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    public float Speed = 5.0f;
    Vector3 tempVect;
    Rigidbody2D rb;
    public Animator anim;
    public bool canMove;
    [HideInInspector]
    public bool facingRight = true;

    PlayerMelee playerMelee;



    void Start()
    {
        playerMelee = GetComponent<PlayerMelee>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        canMove = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!playerMelee.attacking)
        {
            BasicMovement();
        }
        else print("ANGRIBER");
    }

    void BasicMovement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        tempVect = new Vector3(h, v, 0);
        tempVect = tempVect.normalized * Speed * Time.deltaTime;

        if (!this.anim.GetCurrentAnimatorStateInfo(0).IsTag("attack"))
        {
            rb.MovePosition(transform.position += tempVect);
        }

        if (tempVect != Vector3.zero) 
        {
            anim.SetBool("isMoving", true);
            anim.SetFloat("inputX", h);
            anim.SetFloat("inputY", v);


        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        Flip(h);

    }

    void Flip(float horinzontal)
    {
        if(horinzontal > 0 && !facingRight || horinzontal < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;

            theScale.x *= -1;

            transform.localScale = theScale;
        }
    }

}
