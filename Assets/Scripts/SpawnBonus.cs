using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBonus : MonoBehaviour
{
    public Transform spawnPoint1, spawnPoint2;
    //public GameObject lifeBonus;
    //public GameObject[] scoreBonus;
    //public GameObject invulnerabilityBonus;

    public GameObject[] objectsToSpawn;
    public float[] spawnProbabilities;
    public float timeToSpawn;

    void Start()
    {
    	//InvokeRepeating("SpawnObject", timeToSpawn, timeToSpawn);
    }

    public void StartSpawn()
    {
       StartCoroutine("SpawnBonusCor");
    }

    public void StopSpawn()
    {
       StopCoroutine("SpawnBonusCor");
    }

    private IEnumerator SpawnBonusCor()
    {
    	while (true)
        {
            yield return new WaitForSeconds(timeToSpawn);

	    	float totalProbability = 0f;
	        for (int i = 0; i < spawnProbabilities.Length; i++)
	        {
	            totalProbability += spawnProbabilities[i];
	        }
	        
	        for (int i = 0; i < spawnProbabilities.Length; i++)
	        {
	            spawnProbabilities[i] /= totalProbability;
	        }

	        float randomValue = Random.Range(0f, 1f);
	        float randomX = Random.Range(spawnPoint1.position.x, spawnPoint2.position.x);
	        float randomY = Random.Range(spawnPoint1.position.y, spawnPoint2.position.y);

	        float cumulativeProbability = 0f;
	        for (int i = 0; i < objectsToSpawn.Length; i++)
	        {
	            cumulativeProbability += spawnProbabilities[i];
	            
	            if (randomValue <= cumulativeProbability)
	            {
	                GameObject spawnedObject = Instantiate(objectsToSpawn[i], new Vector3(randomX, randomY, 0), Quaternion.identity);
	                Destroy(spawnedObject, 3f);
	                break;
	            }
	        }
	    }
    }
}
