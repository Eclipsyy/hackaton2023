using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    public float delay;
    bool inside;
    PlayerController playerContr;
    private void Awake()
    {
        playerContr = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        Invoke("Shoot", delay);
    }

    void Shoot()
    {
        if (inside)
        {
            playerContr.Dead();
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inside = false;
        }
    }
}
