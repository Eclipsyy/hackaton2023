using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    private void Awake()
    {
        Collider2D col = GetComponent<Collider2D>();
        switch (this.gameObject.name)
        {
            case "LowerBorder":
                transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0, 0)) + Vector3.down * col.bounds.size.y / 2;
                break;
            case "UpperBorder":
                transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height, 0)) - Vector3.down * col.bounds.size.y / 2;
                break;
            case "LeftBorder":
                transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0 , Screen.height / 2, 0)) - Vector3.right * col.bounds.size.x / 2;
                break;
            case "RightBorder":
                transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2, 0)) + Vector3.right * col.bounds.size.x / 2;
                break;
        }
    }
}
