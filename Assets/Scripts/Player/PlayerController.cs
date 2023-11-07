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

    public GameObject[] shooters;
    public GameObject dog;
    public Animator animDog;
    private Animator animPlayer;

    private bool isDead = false;

    private Vector2 mousePos;
    private bool isMouseDown;
    public float offset;

    float vert;
    float hor;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        animPlayer = GetComponent<Animator>();
        dog = GameObject.Find("Dog2Anchor");
        animDog = dog.GetComponentInChildren<Animator>();
        shooters = GameObject.FindGameObjectsWithTag("Shooter");
        foreach (var shooter in shooters)
        {
            shooter.GetComponent<Shooter>().StartShooting();
        }
        //Shooter.instance.StartShooting();
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

    private void Update()
    {
    	mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    	if (Input.GetMouseButtonDown(0))
        {
            isMouseDown = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
        }
    	
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

            if (isMouseDown)
    	    {
	    		//Instantiate(test, new Vector3(mousePos.x, mousePos.y, 0), Quaternion.identity);
	    		if (mousePos.x > transform.position.x + offset)
	    		{
	    			JumpRight();
	    		}
	    		else if (mousePos.x < transform.position.x - offset)
	    		{
	    			JumpLeft();
	    		}
	    		else
	    		{
	    			JumpUp();
	    		}
    	    }
        }
    }

    public void Dead()
    {
        isDead = true;
        //dog.transform.position = new Vector3(transform.position.x, dog.transform.position.y, 0);
        animPlayer.SetTrigger("isDead");
        GetComponent<AudioSource>().Play();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ground")
        {
            dog.transform.position = new Vector3(transform.position.x, dog.transform.position.y, 0);
            animDog.SetTrigger("isCatch");
            foreach (var shooter in shooters)
            {
                shooter.GetComponent<Shooter>().StopShooting();
            }
            //Shooter.instance.StopShooting();
            GameManager.instance.Respawn();
            Destroy(gameObject);
        }
    }
}
