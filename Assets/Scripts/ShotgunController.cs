using System;
using UnityEngine;

public class ShotgunController : IWeapon {

    [Header("Settings")]
    public int damage;
    public float bulletSpeed;
    public float fireInterval = .5f;
    public string damageTag = "Enemy";
    public int numberBullets;
    public float angle;

    [Header("Configurations")]
    public Transform fireOrigin;
    public GameObject bulletPrototype;

    [Header("Status (Do not modify these fields through Editor)")]
    public float lastFireTime;

    public override void OnTriggerPressed() {
        if (CanFire()) {
            Fire();
        }
    }
    
    public override bool CanFire() {
        var time = Time.time;
        return lastFireTime + fireInterval <= time;
    }

    private void Fire() {
        lastFireTime = Time.time;
        float angleInRad = Mathf.Deg2Rad * angle;
        float startAngle = -1 * Vector2.SignedAngle(transform.up, new Vector2(1, 0)) * Mathf.Deg2Rad;
        float firstDegree = startAngle - angleInRad / 2;
        float degreesBetween = angleInRad / (numberBullets - 1);
        
        float ang = firstDegree;
        for (int i = 0; i < numberBullets; i++) {
            var bullet = Instantiate(bulletPrototype, fireOrigin ? fireOrigin.position : transform.position, transform.rotation);
            var controller = bullet.GetComponent<BulletController>();
            controller.damage = damage;
            controller.damageTag = damageTag;
            float xVel = Mathf.Cos(ang);
            float yVel = Mathf.Sin(ang);
            controller.GetComponent<Rigidbody2D>().velocity = new Vector2(xVel, yVel) * bulletSpeed;
            ang += degreesBetween;

        }

        AudioManager.PlayAtPoint(fireSfx, fireOrigin ? fireOrigin.position : transform.position);
    }
}
