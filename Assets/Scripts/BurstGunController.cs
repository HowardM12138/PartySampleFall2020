using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BurstGunController : IWeapon {

	[Header("Settings")]
	public int damage;
	public float bulletSpeed;
	public float fireInterval = .5f;
	public string damageTag = "Enemy";

	[Header("Configurations")]
	public Transform fireOrigin;
	public GameObject bulletPrototype;

	[Header("Status (Do not modify these fields through Editor)")]
	public float lastFireTime;

	public override void OnTriggerPressed() {
		Trigger();
	}

	public override void SwitchOff() {
		StopAllCoroutines();
		base.SwitchOff();
	}

	public void Trigger() {
		if (CanFire()) {
			StartCoroutine(Fire());
		}
	}

	public override bool CanFire() {
		var time = Time.time;
		return lastFireTime + fireInterval <= time;
	}

	IEnumerator Fire() {
		var wait = new WaitForSeconds(.07f);
		for (float i = 0f; i < 3f; i++){
            lastFireTime = Time.time;
            var bullet = Instantiate(bulletPrototype, fireOrigin ? fireOrigin.position : transform.position, transform.rotation);
            var controller = bullet.GetComponent<BulletController>();
            controller.damage = damage;
            controller.damageTag = damageTag;
            var velocity = bullet.transform.up * bulletSpeed;
            controller.GetComponent<Rigidbody2D>().velocity = velocity;
            AudioManager.PlayAtPoint(fireSfx, transform.position);
            yield return wait;
		}
	}
}