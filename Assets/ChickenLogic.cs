using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = transform.position + Vector3.right;
        }
    }
}
