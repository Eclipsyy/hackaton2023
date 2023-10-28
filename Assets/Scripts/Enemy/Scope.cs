using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    public float delay;
    private void Awake()
    {
        Invoke("Shoot", delay);
    }

    void Shoot()
    {

        Destroy(gameObject);
    }
}
