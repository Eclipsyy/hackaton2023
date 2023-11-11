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
    private GameObject spawner;
    public GameObject dog;
    public Animator animDog;
    private Animator animPlayer;

    private bool isDead = false;

    private Vector2 mousePos;
    private bool isMouseDown;
    public float offset;

    public bool invulnerability;
    public float timer = 0;
    private GameObject eff;

    float vert;
    float hor;

    private void Awake()
    {
        eff = gameObject.transform.Find("Eff").gameObject;
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        animPlayer = GetComponent<Animator>();
        dog = GameObject.Find("Dog2Anchor");
        animDog = dog.GetComponentInChildren<Animator>();

        shooters = GameObject.FindGameObjectsWithTag("Shooter");
        spawner = GameObject.Find("Spawner");


        foreach (var shooter in shooters)
        {
            Shooter sh = shooter.GetComponent<Shooter>();
            if (!sh.isBomb)
            {
                sh.StartShooting();
            }
        }
        spawner.GetComponent<SpawnBonus>().StartSpawn();
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
        if (timer > 0)
        {
            timer -= Time.deltaTime; 
            invulnerability = true;
            eff.SetActive(true);
        }
        else
        {
            invulnerability = false;
            timer = 0;
            eff.SetActive(false);
        }

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
        if (!invulnerability)
        {
            isDead = true;
            if (animPlayer != null)
            {
               animPlayer.SetTrigger("isDead"); 
            }
            GetComponent<AudioSource>().Play();
        }
       
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
            spawner.GetComponent<SpawnBonus>().StopSpawn();
            //Shooter.instance.StopShooting();
            GameManager.instance.Respawn();
            Destroy(gameObject);
        }
    }
}
