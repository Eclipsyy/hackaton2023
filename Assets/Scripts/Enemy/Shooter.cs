using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject scope;
    public Transform playerTransform;
    Vector3 playerPosition;
    //public float innerRadius;
    //public float outerRadius;
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
        //InvokeRepeating("Shoot", shootRate, shootRate);
    }

    private Vector3 Aim()
    {
        //float outerX = (Random.value * outerRadius * 2) - outerRadius;
        //float innerX = outerX * innerRadius / outerRadius;
        //float outerY = (Random.value < 0.5) ? Mathf.Sqrt(outerRadius * outerRadius - outerX * outerX) : -1 * Mathf.Sqrt(outerRadius * outerRadius - outerX * outerX);
        //float innerY = outerY * innerRadius / outerRadius;

        //Vector3 OuterVector = new Vector3(outerX, outerY, 0);
        //Vector3 InnerVector = new Vector3(innerX, innerY, 0);

        //Vector3 InterpolatedVector = Vector3.Lerp(OuterVector, InnerVector, Random.value);

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
