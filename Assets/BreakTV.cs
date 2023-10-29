using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakTV : MonoBehaviour
{
	public void Awake()
	{
		Invoke("Hit", 35);
	}

	public void Hit(){
		//gameobject.SetActive(true);
	}
}
