using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject playerGO;
    public GameObject ground;
    public int lifes;
    public float respUpDist;
    public float respInitSpeed;
    public float respDelay;

    private void Awake()
    {
        instance = this;
        StartCoroutine(GameManager.instance.RespawnCor());
    }
    public void GameOver()
    {
        SceneManager.LoadScene(0);
    }

    public void Respawn()
    {
        lifes -= 1;
        if (lifes == 0)
        {
            GameOver();
            return;
        }
        StartCoroutine(GameManager.instance.RespawnCor());
    }

    public IEnumerator RespawnCor()
    {
        yield return new WaitForSeconds(respDelay);
        GameObject resp = Instantiate(playerGO);
        resp.transform.position = ground.transform.position + Vector3.up * respUpDist;
        resp.GetComponent<Rigidbody2D>().velocity = Vector3.up * respInitSpeed;
    }
}
