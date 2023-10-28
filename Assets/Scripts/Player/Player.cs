using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static GameObject player;

    private void Awake()
    {
        player = GetComponent<GameObject>();
    }

    void GetHit()
    {

    }
}
