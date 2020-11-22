using UnityEngine;

public class MissileLauncherController : IWeapon {
	[Header("Settings")]
	public float bulletSpeed;
	public float fireInterval = .5f;
	public int damage = 40;
	public float radius = 10;
	public float steer = 21;
	public string damageTag = "Enemy";

	[Header("Configurations")]
	public Transform fireOrigin;
	public GameObject missilePrototype;

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
		var missile = Instantiate(missilePrototype, fireOrigin ? fireOrigin.position : transform.position, transform.rotation);
		var controller = missile.GetComponent<MissileController>();
		controller.damageTag = damageTag;
		controller.speed = bulletSpeed;
		controller.damage = damage;
		controller.radius = radius;
		controller.steer = steer;
		var velocity = missile.transform.up * bulletSpeed;
		controller.GetComponent<Rigidbody2D>().velocity = velocity;
		AudioManager.PlayAtPoint(fireSfx, missile.transform.position);
	}

}