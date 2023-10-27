using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public Transform firePoint;
	public GameObject bulletPrefab;
	public float bulletForce;
	public float intervalShoot;
	private float timeShoot = 0;

	public float offset;
    public Transform playerTransform;

    void Update()
    {
    	Vector3 difference = playerTransform.position-transform.position;
    	float rotateZ = Mathf.Atan2(difference.y, difference.x)*Mathf.Rad2Deg;
    	firePoint.transform.rotation = Quaternion.Euler(0, 0, rotateZ+offset);
        
    	if (timeShoot<=0)
    	{
    		GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    		Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        	rb.AddForce(firePoint.up*bulletForce, ForceMode2D.Impulse);
    	    timeShoot = intervalShoot;
    	}
    	else
    	{
    		timeShoot -= Time.deltaTime;
    	}
    }
}
