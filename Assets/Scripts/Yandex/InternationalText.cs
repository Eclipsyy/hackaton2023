using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InternationalText : MonoBehaviour
{

	[SerializeField] string _en;
	[SerializeField] string _ru;
	[SerializeField] string _tr;
    [SerializeField] string _es;

    void Start()
    {
        if (Language.Instance.CurrentLanguage == "en")
        {
        	GetComponent<TextMeshProUGUI>().text = _en;
        }
        else if (Language.Instance.CurrentLanguage == "ru")
        {
        	GetComponent<TextMeshProUGUI>().text = _ru;
        }
        else if (Language.Instance.CurrentLanguage == "tr")
        {
        	GetComponent<TextMeshProUGUI>().text = _tr;
        }
        else if (Language.Instance.CurrentLanguage == "es")
        {
            GetComponent<TextMeshProUGUI>().text = _es;
        }
        else
        {
        	GetComponent<TextMeshProUGUI>().text = _en;
        }
    }
}
