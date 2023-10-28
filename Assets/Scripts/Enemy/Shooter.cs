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
        InvokeRepeating("CreateScope", shootRate, shootRate);
    }

    private void CreateScope()
    {
        float outerX = (Random.value * outerRadius * 2) - outerRadius;
        float innerX = outerX * innerRadius / outerRadius;
        float outerY = (Random.value < 0.5) ? Mathf.Sqrt(outerRadius * outerRadius - outerX * outerX) : -1*Mathf.Sqrt(outerRadius * outerRadius - outerX * outerX);
        float innerY = outerY * innerRadius / outerRadius;

        Vector3 OuterVector = new Vector3(outerX, outerY, 0);
        Vector3 InnerVector = new Vector3(innerX, innerY, 0);

        Vector3 InterpolatedVector = Vector3.Lerp(OuterVector, InnerVector, Random.value);

        playerPosition = playerTransform.position;
        Vector3 ScopePosition = playerPosition + InterpolatedVector;

        GameObject createdScope = GameObject.Instantiate(Scope);
        createdScope.transform.position = ScopePosition;
    }

}
