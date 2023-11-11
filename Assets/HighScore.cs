using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{
	private string str;
	
    private void Start()
    {
    	switch (Language.Instance.CurrentLanguage)
		{
	    case "en":
	        str = "TOP SCORE = ";
	        break;
	    case "ru":
	        str = "РЕКОРД = ";
	        break;
	    case "tr":
	        str = "EN YÜKSEK PUAN = ";
	        break;
	    case "es":
	        str = "PUNTUACIÓN MÁXIMA = ";
	        break;
	    default:
	        str = "TOP SCORE = ";
	        break;
		}

		GetComponent<TextMeshProUGUI>().text = str + Progress.Instance.PlayerInfo.HighScore.ToString("D6");
    }
}
