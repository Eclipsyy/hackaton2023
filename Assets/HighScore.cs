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
        	GetComponent<TextMeshProUGUI>().text = "TOP SCORE = " + Progress.Instance.PlayerInfo.HighScore.ToString("D6"); //+ SaveSystem.ss.highScore.ToString("D6");
        }
        else if (Language.Instance.CurrentLanguage == "ru")
        {
        	GetComponent<TextMeshProUGUI>().text = "РЕКОРД = " + Progress.Instance.PlayerInfo.HighScore.ToString("D6"); //+ SaveSystem.ss.highScore.ToString("D6");
        }
        else if (Language.Instance.CurrentLanguage == "tr")
        {
        	GetComponent<TextMeshProUGUI>().text = "EN YÜKSEK PUAN = " + Progress.Instance.PlayerInfo.HighScore.ToString("D6"); //+ SaveSystem.ss.highScore.ToString("D6");
        }
        else if (Language.Instance.CurrentLanguage == "es")
        {
        	GetComponent<TextMeshProUGUI>().text = "PUNTUACIÓN MÁXIMA = " + Progress.Instance.PlayerInfo.HighScore.ToString("D6"); //+ SaveSystem.ss.highScore.ToString("D6");
        }
        else
        {
        	GetComponent<TextMeshProUGUI>().text = "TOP SCORE = " + Progress.Instance.PlayerInfo.HighScore.ToString("D6"); //+ SaveSystem.ss.highScore.ToString("D6");
        }
    }
}
