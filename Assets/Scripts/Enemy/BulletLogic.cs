using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
    	Destroy(gameObject);
    }
}
