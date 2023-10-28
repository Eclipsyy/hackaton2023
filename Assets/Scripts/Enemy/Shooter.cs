using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
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

    private void Awake()
    {
        shootTimer = shootRate;
        weightSum = rifleWeight + shotgunWeight + laserWeight;
    }

    private Vector3 Aim()
    {
        Vector2 bias2D = Random.insideUnitCircle * aimRadius;
        Vector3 ShootBias = new Vector3(bias2D.x, bias2D.y, 0);
        playerPosition = playerTransform.position;
        Vector3 AimPosition = playerPosition + ShootBias;

        return AimPosition;
    }

    private void Shoot()
    {
        float seed = Random.Range(0, weightSum);
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

    private void Update()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer < 0 )
        {
            Shoot();
            shootTimer = shootRate;
        }
    }
}
