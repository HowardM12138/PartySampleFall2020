using System;
using UnityEngine;

public class GrenadeThrowerController : IWeapon {
	
	[Header("Settings")]
	public int damage;
	public float bulletSpeed;
	public float timeToExplode = 3;
	public float radius = 3;
	public float fireInterval = .5f;
	public string damageTag = "Enemy";

	[Header("Configurations")]
	public Transform fireOrigin;
	public GameObject grenadePrototype;

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
		var grenade = Instantiate(grenadePrototype, fireOrigin ? fireOrigin.position : transform.position, transform.rotation);
		var controller = grenade.GetComponent<GrenadeController>();
		controller.damage = damage;
		controller.damageTag = damageTag;
		controller.timeToExplode = timeToExplode;
		controller.radius = radius;
		var velocity = grenade.transform.up * bulletSpeed;
		controller.GetComponent<Rigidbody2D>().velocity = velocity;
		AudioManager.PlayAtPoint(fireSfx, grenade.transform.position);
	}
}
