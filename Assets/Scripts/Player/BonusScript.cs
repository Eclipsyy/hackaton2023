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
	public AudioClip audio;
	//private PlayerController playerContr;

	void Awake()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

    void Update()
    {
        transform.Rotate(new Vector3(0, speedRoate, 0));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
        	GameObject.Find("AudioGameplay").GetComponent<AudioSource>().PlayOneShot(audio);

        	gameManager.score += addScore;

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
