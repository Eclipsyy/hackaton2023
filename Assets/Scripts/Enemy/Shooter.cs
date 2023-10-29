using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public static Shooter instance;

    public GameObject scope;
    public Transform playerTransform;
    Vector3 playerPosition;
    public float shootRate;
    float shootTimer;
    public float aimRadius;
    public int shotgunShots;
    public float shotgunSpread;
    public float shotgunScale;

    public float rifleWeight;
    public float shotgunWeight;
    public float laserWeight;
    float weightSum;

    public GameObject handPistol;
    private float targetX;
    public float speedHand;

    private void Awake()
    {
        instance = this;
        shootTimer = shootRate;
        weightSum = rifleWeight + shotgunWeight + laserWeight;
    }

    public void StartShooting()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        StartCoroutine("ShootCor");
    }

    public void StopShooting()
    {
        StopCoroutine("ShootCor");
    }

    private Vector3 Aim()
    {

        Vector2 bias2D = Random.insideUnitCircle * aimRadius;
        Vector3 ShootBias = new Vector3(bias2D.x, bias2D.y, 0);
        playerPosition = playerTransform.position;
        Vector3 AimPosition = playerPosition + ShootBias;

        return AimPosition;
    }

    public IEnumerator TestCor()
    {
        yield return null;
        Debug.Log(Time.deltaTime);
    }

    public IEnumerator ShootCor()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootRate);
            float seed = Random.Range(0, weightSum);
            /*
            if (seed < rifleWeight)
            {
                ShootRifle();
            }
            else if (seed >= rifleWeight && seed < shotgunWeight + rifleWeight)
            {
                ShootShotgun();
            }
            else
            {
                ShootLaser();
            }
            */
            ShootPistol();
        }
        
    }

    private void ShootPistol()
    {
        Vector3 ScopePosition = Aim();

        GameObject createdScope = GameObject.Instantiate(scope);
        createdScope.transform.position = ScopePosition;
        targetX = createdScope.transform.position.x;

    }

    private void ShootRifle()
    {
        Vector3 ScopePosition = Aim();

        GameObject createdScope = GameObject.Instantiate(scope);
        createdScope.transform.position = ScopePosition;
    }

    private void ShootShotgun()
    {
        Vector3 ScopePosition = Aim();

        for (int i =0; i < shotgunShots; i++)
        {
            GameObject shot = Instantiate(scope);
            Vector2 bias = Random.insideUnitCircle * shotgunSpread;
            shot.transform.position = new Vector3(bias.x, bias.y, 0) + playerPosition;
            shot.transform.localScale = Vector3.one * shotgunScale;
        }
    }

    private void ShootLaser()
    {

    }

    private void FixedUpdate()
    {
        handPistol.transform.position = Vector3.Lerp(handPistol.transform.position, new Vector3(targetX, handPistol.transform.position.y, 0), Time.deltaTime * speedHand);
    }


}
