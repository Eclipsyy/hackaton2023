using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Image[] healthBar;
    public Sprite lostHealth;
    public Sprite fullHealth;
    public GameObject playerGO;
    public GameObject ground;
    public TextMeshProUGUI scoreText;
    public int lifes;
    public float respUpDist;
    public float respInitSpeed;
    public float respDelay;
    public float scoreDelay;
    public int scoreAddition;
    public int score = 0;
    public int countMiss;
    public float gamemodeMultiplyer;

    public bool isGameOver = false;
    public GameObject canvGO;
    public Animator noiseAnim;

    //public AudioSource hitAudio;

    private void Awake()
    {
        instance = this;
        StartCoroutine(RespawnCor());
        StartCoroutine(ScoreCor());
    }
    public void GameOver()
    {
        isGameOver = true;
        //SaveSystem.ss.SaveGame();
        //SaveSystem.ss.score = 0;
        if (score > Progress.Instance.PlayerInfo.HighScore)
        {
            Progress.Instance.PlayerInfo.HighScore = score;
            Progress.Instance.Save();
            Progress.Instance.Leader("HighScore", score);
        }
        score = 0;
        StopCoroutine(ScoreCor());
        canvGO.SetActive(true);
        noiseAnim.SetTrigger("isAppear");
        StartCoroutine(GameOverCor());
        //SceneManager.LoadScene(0);
    }

    public IEnumerator GameOverCor()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
    }

    public void AddLife()
    {
        if (lifes < 5)
        {
           healthBar[lifes].sprite = fullHealth;
           lifes += 1; 
        }
    }

    public void Respawn()
    {
        //StopCoroutine(ScoreCor());
        //hitAudio.PlayOneShot(hitAudio.clip);
        lifes -= 1;
        countMiss = 0;
        healthBar[lifes].sprite = lostHealth;
        if (lifes == 0)
        {
            GameOver();
            return;
        }
        StartCoroutine(RespawnCor());
        
    }

    public IEnumerator RespawnCor()
    {
        yield return new WaitForSeconds(respDelay);
        GameObject resp = Instantiate(playerGO);
        resp.transform.position = ground.transform.position + Vector3.up * respUpDist;
        resp.GetComponent<Rigidbody2D>().velocity = Vector3.up * respInitSpeed;
        //StartCoroutine(ScoreCor());
    }

    public IEnumerator ScoreCor()
    {
        while (true)
        {
            yield return new WaitForSeconds(scoreDelay);
            //SaveSystem.ss.score += (int)(scoreAddition * gamemodeMultiplyer);
            score += (int)(scoreAddition * gamemodeMultiplyer);
            scoreText.text = score.ToString("D6"); //SaveSystem.ss.score.ToString("D6");
        }
    }

    public void AwardAdv()
    {
        Progress.Instance.RewardedAdv();
    } 

}
