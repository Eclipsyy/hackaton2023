using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioGameplay : MonoBehaviour
{
	public AudioSource QuackAudio;

	void Awake()
	{
		InvokeRepeating("Quack", 5f, 5f);
	}

    public void Quack()
    {
    	if (!GameManager.instance.isGameOver)
    	{
    		QuackAudio.PlayOneShot(QuackAudio.clip);
    	}
    }
}
