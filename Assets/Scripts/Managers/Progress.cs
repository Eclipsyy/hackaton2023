using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class PlayerInfo
{
	public int HighScore = 0;
}

public class Progress : MonoBehaviour
{
	public PlayerInfo PlayerInfo;
	public static Progress Instance;
	public bool WatchedStartAdv = false;
	public GameObject[] awardAdvButton;
	public int lastScore = 0;

    [DllImport("__Internal")]
    private static extern void SaveExtern(string date);

    [DllImport("__Internal")]
    private static extern void LoadExtern();

    [DllImport("__Internal")]
    private static extern void SetToLeaderboard(string name, float value);

    [DllImport("__Internal")]
    private static extern void FullAdvExtern();

    [DllImport("__Internal")]
    private static extern void RewardedAdvExtern();

    void Awake()
    {
    	if (Instance==null)
    	{
    		transform.parent = null;
    		DontDestroyOnLoad(gameObject);
    		Instance = this;
    		LoadExtern();
            //Load();
    	} else
    	{
    		Destroy(gameObject);
    	}
    }

    void Start()
    {
        if (WatchedStartAdv==false)
        {
            Progress.Instance.FullAdv();
            WatchedStartAdv=true;
        }
    }

    void Update()
    {

    }


    public void Save()
    {
    	string JsonString = JsonUtility.ToJson(PlayerInfo);
    	SaveExtern(JsonString);
    }
    
    public void SetPlayerInfo(string value)
    {
    	PlayerInfo = JsonUtility.FromJson<PlayerInfo>(value);
    }

    public void FullAdv()
    {
        AudioListener.pause = true;
        Time.timeScale = 0f;
        FullAdvExtern();
    }

    public void ContinOnClosed()
    {
    	Time.timeScale = 1f;
        AudioListener.pause = false;
        Button button = GameObject.FindWithTag("AwardButton").GetComponent<Button>();
        if (button != null)
        {
        	button.interactable = true;
        }
    }

    public void Leader(string name, int scor)
    {
    	SetToLeaderboard(name, scor);
    }

    public void RewardedAdv()
    {
        Time.timeScale = 0f;
    	AudioListener.pause = true;
    	Button button = GameObject.FindWithTag("AwardButton").GetComponent<Button>();
    	if (button != null)
        {
        	button.interactable = false;
        }
        
        RewardedAdvExtern();
    }

    public void GetReward()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.Revival();
    }
}
