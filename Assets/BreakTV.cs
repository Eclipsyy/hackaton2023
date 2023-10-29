using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakTV : MonoBehaviour
{
	public GameObject go;
	public void Awake()
	{
		Invoke("Hit", 40);
	}

	public void Hit(){
		go.SetActive(true);
	}
}
