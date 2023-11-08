using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusScript : MonoBehaviour
{
	public float speedRoate;
	public int addScore;

    void Update()
    {
        transform.Rotate(new Vector3(0, speedRoate, 0));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
