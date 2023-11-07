using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class Language : MonoBehaviour
{
	public static Language Instance;

	//[DllImport("__Internal")]
    //private static extern string GetLang();

	public string CurrentLanguage;

    private void Awake()
    {
    	if (Instance==null)
    	{
    		Instance = this;
    		DontDestroyOnLoad(gameObject);

    		//CurrentLanguage = GetLang();
    		CurrentLanguage = "ru";
    	}
    	else
    	{
    		Destroy(gameObject);
    	}
    }
}
