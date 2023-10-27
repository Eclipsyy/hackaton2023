using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float horizontalMovementScale;
    public float verticalMovementScale;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void JumpLeft()
    {
        JumpUp();
        rb.velocity += Vector2.left * horizontalMovementScale;
    }

    public void JumpRight()
    {
        JumpUp();
        rb.velocity += Vector2.right * horizontalMovementScale;
    }

    public void JumpUp()
    {
        rb.velocity += Vector2.up * verticalMovementScale;
    }

    public void JumpLeftMod()
    {
        JumpUp();
        rb.velocity = new Vector2(-horizontalMovementScale ,rb.velocity.y);
    }

    public void JumpRightMod()
    {
        JumpUp();
        rb.velocity = new Vector2(horizontalMovementScale, rb.velocity.y);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            JumpUp();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            JumpLeftMod();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            JumpRightMod();
        }
    }
}
