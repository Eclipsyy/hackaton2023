using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusScript : MonoBehaviour
{
	public float speedRoate;
	public int addScore;
	public bool isInvulnerability;
	public bool isLife;

	private GameManager gameManager;
	public Transform child;
	public AudioClip audio;

	void Awake()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

    void Update()
    {
        transform.Rotate(new Vector3(0, speedRoate * Time.deltaTime, 0));
        //Transform child = transform.GetChild(0);
        if (child != null)
        {
        	child.Rotate(new Vector3(0, -speedRoate * Time.deltaTime, 0));
        }
        //transform.RotateAround(transform.position, new Vector3(0, 1, 0), speedRoate * Time.deltaTime);
        //transform.rotation *= Quaternion.Euler(0, speedRoate * Time.deltaTime, 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
        	GameObject.Find("AudioGameplay").GetComponent<AudioSource>().PlayOneShot(audio);

        	if (gameManager.score + addScore >= 0)
        	{
        		gameManager.score += addScore;
        	}
        	else
        	{
        		gameManager.score = 0;
        	}
        	

        	if (isLife)
        	{
        		gameManager.AddLife();
        	}

        	if (isInvulnerability)
        	{
        		PlayerController playerContr = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        		playerContr.timer = 7f;
        	}

            Destroy(gameObject);
        }
    }
}
