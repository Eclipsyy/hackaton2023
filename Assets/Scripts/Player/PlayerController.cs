using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float horizontalMovementScale;
    public float verticalMovementScale;
    public float jumpModifier;

    private SpriteRenderer rend;

    public GameObject dog;
    public Animator animDog;
    private Animator animPlayer;

    private bool isDead = false;

    float vert;
    float hor;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        animPlayer = GetComponent<Animator>();
        dog = GameObject.Find("Dog2Anchor");
        animDog = dog.GetComponentInChildren<Animator>();
        Shooter.instance.StartShooting();
    }

    public void JumpUp()
    {
        rb.velocity = Vector2.up * verticalMovementScale * jumpModifier;
    }

    public void JumpLeft()
    {
        rend.flipX = true;
        rb.velocity = new Vector2(-horizontalMovementScale, verticalMovementScale);
    }

    public void JumpRight()
    {
        rend.flipX = false;
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
    }

    public void Dead()
    {
        isDead = true;
        //dog.transform.position = new Vector3(transform.position.x, dog.transform.position.y, 0);
        animPlayer.SetTrigger("isDead");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ground")
        {
            dog.transform.position = new Vector3(transform.position.x, dog.transform.position.y, 0);
            animDog.SetTrigger("isCatch");
            Shooter.instance.StopShooting();
            GameManager.instance.Respawn();
            Destroy(gameObject);
        }
    }
}
