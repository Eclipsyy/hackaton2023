using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance;
	public AudioSource buttonClickAudio;

	void Awake()
    {
    	if (Instance==null)
    	{
    		transform.parent = null;
    		DontDestroyOnLoad(gameObject);
    		Instance = this;
    	} else
    	{
    		Destroy(gameObject);
    	}
    }

    public void OnButtonClick()
    {
    	buttonClickAudio.PlayOneShot(buttonClickAudio.clip);
    }
}
