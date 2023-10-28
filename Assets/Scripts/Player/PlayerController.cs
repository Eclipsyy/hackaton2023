using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float horizontalMovementScale;
    public float verticalMovementScale;
    public float jumpModifier;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            JumpUp();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            JumpLeft();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            JumpRight();
        }
    }
}
