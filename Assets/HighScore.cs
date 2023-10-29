using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    private void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "TOP SCORE = " + SaveSystem.ss.highScore.ToString("D6");
    }
}
