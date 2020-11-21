using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    [Header("Settings")]
	public KeyCode fireKey = KeyCode.Mouse0;
    public int damage;
	public string damageTag = "Enemy";
    public float chargeTime = 3f;

    [Header("Configurations")]
    public Transform firePoint;
    public GameObject bulletPrefab;

    // Private Variables
    private float charge = 0f;
    
    void Update()
    {
        if(Input.GetKey(fireKey))
        {
            charge += Time.deltaTime;
            //Debug.Log("Charge: " + charge);
        }

        if(Input.GetKeyUp(fireKey))
        {
            charge = 0f;
        }

        if(charge >= chargeTime)
        {
            Shoot();
        }
    }

    private void Shoot()
    { 
        var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        var controller = bullet.GetComponent<LaserBulletController>();
        controller.damage = damage;
        controller.damageTag = damageTag;

        charge = 0f;
    }
}
