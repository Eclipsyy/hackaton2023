using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    public GameObject prefEff;
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
        GameObject shot = Instantiate(prefEff, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        //shot.GetComponent<Animator>().SetTrigger("isShoot");
        Destroy(shot, 5f);

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
