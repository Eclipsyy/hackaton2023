using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static GameObject player;
    public int lifes;

    private void Awake()
    {
        player = GetComponent<GameObject>();
    }

    void GetHit()
    {
        lifes -= 1;
        if ( lifes == 0)
        {
            GameManager.instance.GameOver();
        }
    }
}
