using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombLogic : MonoBehaviour
{
	public float speedFall;
	public AudioClip boomAudio;

	void Start()
	{
		Scope scope = gameObject.GetComponent<Scope>();
		Invoke("Boom", scope.delay);
	}

    void Update()
    {
        transform.position += -Vector3.up * speedFall * Time.deltaTime;
    }

    void Boom()
    {
    	GameObject.Find("AudioGameplay").GetComponent<AudioSource>().PlayOneShot(boomAudio);
        Animator fon = GameObject.Find("BackgroundGame1").GetComponent<Animator>();
        fon.SetTrigger("isBoom");
    }
}
