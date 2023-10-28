using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject Scope;
    public Transform playerTransform;
    Vector3 playerPosition;
    public float innerRadius;
    public float outerRadius;
    public float shootRate;

    private void Awake()
    {
        playerPosition = playerTransform.position;
        InvokeRepeating("CreateScope", shootRate, shootRate);
    }

    private void CreateScope()
    {
        float outerX = (Random.value * outerRadius * 2) - outerRadius;
        float innerX = outerX * innerRadius / outerRadius;
        float outerY = Mathf.Sqrt(outerRadius * outerRadius - outerX * outerX);
        float innerY = Mathf.Sqrt(innerRadius * innerRadius - innerX * innerX);

        Vector3 OuterVector = new Vector3(outerX, outerY, 0);
        Vector3 InnerVector = new Vector3(innerX, innerY, 0);

        Vector3 InterpolatedVector = Vector3.Lerp(OuterVector, InnerVector, Random.value);

        Vector3 ScopePosition = playerPosition + InterpolatedVector;

        GameObject createdScope = GameObject.Instantiate(Scope);

        //createdScope.transform
    }

}
