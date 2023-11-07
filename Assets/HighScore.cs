using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    private void Start()
    {
        if (Language.Instance.CurrentLanguage == "en")
        {
        	GetComponent<TextMeshProUGUI>().text = "TOP SCORE = " + SaveSystem.ss.highScore.ToString("D6");
        }
        else if (Language.Instance.CurrentLanguage == "ru")
        {
        	GetComponent<TextMeshProUGUI>().text = "РЕКОРД = " + SaveSystem.ss.highScore.ToString("D6");
        }
        else if (Language.Instance.CurrentLanguage == "tr")
        {
        	GetComponent<TextMeshProUGUI>().text = "TOP SCORE = " + SaveSystem.ss.highScore.ToString("D6");
        }
        else
        {
        	GetComponent<TextMeshProUGUI>().text = "TOP SCORE = " + SaveSystem.ss.highScore.ToString("D6");
        }
    }
}
