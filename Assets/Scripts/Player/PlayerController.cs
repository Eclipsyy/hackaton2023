using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float horizontalMovementScale;
    public float verticalMovementScale;
    public float jumpModifier;

    public GameObject dog;
    public Animator animDog;
    private Animator animPlayer;

    private bool isDead = false;

    float vert;
    float hor;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animPlayer = GetComponent<Animator>();
    }

    public void JumpUp()
    {
        rb.velocity = Vector2.up * verticalMovementScale * jumpModifier;
    }

    public void JumpLeft()
    {
        rb.velocity = new Vector2(-horizontalMovementScale, verticalMovementScale);
    }

    public void JumpRight()
    {
        rb.velocity = new Vector2(horizontalMovementScale, verticalMovementScale);
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            vert = Input.GetAxis("Vertical");
            if (vert > 0)
            {
                JumpUp();
                return;
            }
            hor = Input.GetAxis("Horizontal");
            if (hor < 0)
            {
                JumpLeft();
                return;
            }
            else if (hor > 0)
            {
                JumpRight();
                return;
            }
        }
        vert = Input.GetAxis("Vertical");
        if (vert > 0)
        {
            JumpUp();
            return;
        }
    }

    //private void Update()
    //{
    //    if (rb.velocity.x < 0)
    //    {
    //        Get
    //    }
    //}
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Scope")
        {
           isDead = true;
        dog.transform.position = new Vector3(transform.position.x, dog.transform.position.y, 0);
        animPlayer.SetTrigger("isDead");
        animDog.SetTrigger("isCatch"); 
        }
    }
}
