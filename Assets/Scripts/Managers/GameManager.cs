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
    public GameObject playerGO;
    public GameObject ground;
    public TextMeshProUGUI scoreText;
    public int lifes;
    public float respUpDist;
    public float respInitSpeed;
    public float respDelay;
    public float scoreDelay;
    public int scoreAddition;

    private void Awake()
    {
        instance = this;
        StartCoroutine(RespawnCor());
        StartCoroutine(ScoreCor());
    }
    public void GameOver()
    {
        SaveSystem.ss.SaveGame();
        SaveSystem.ss.score = 0;
        StopCoroutine(ScoreCor());
        SceneManager.LoadScene(0);
    }

    public void Respawn()
    {
        //StopCoroutine(ScoreCor());
        lifes -= 1;
        Shooter.instance.countMiss = 0;
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
            SaveSystem.ss.score += scoreAddition;
            scoreText.text = SaveSystem.ss.score.ToString("D6");
        }
    }
}
