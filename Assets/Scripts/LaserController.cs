using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : IWeapon {
    [Header("Settings")]
    public int damage;
	public string damageTag = "Enemy";
    public float chargeTime = 3f;

    [Header("Configurations")]
    public LaserSight sight;
    public Transform firePoint;
    public GameObject bulletPrefab;

    // Private Variables
    private float chargeStartTime;
    private bool charging;

    public override void OnTriggerPressed() {
        if (charging != true) {
            chargeStartTime = Time.time;
        }
        charging = true;
        sight.OnTriggerPressed();
    }

    public override void OnTriggerReleased() {
        charging = false;
        chargeStartTime = Time.time;
        sight.OnTriggerReleased();
    }

    public override void SwitchOff() {
        OnTriggerReleased();
        base.SwitchOff();
    }

    void Update() {
        if (charging) {
            if (Time.time - chargeStartTime >= chargeTime) {
                Shoot();
            }
        }
    }

    private void Shoot() {
        var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        var controller = bullet.GetComponent<LaserBulletController>();
        controller.damage = damage;
        controller.damageTag = damageTag;
        charging = false;
        AudioManager.PlayAtPoint(fireSfx, transform.position);
    }
}
