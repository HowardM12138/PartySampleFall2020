﻿using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Health : MonoBehaviour {
	
	[Header("Settings")]
	public int maxHealth;
	public AudioClip deathSfx;
	public UnityEvent onDeath;

	[Header("Configurations")]
	public Transform healthBar;
	public Transform healthMask;

	[Header("Status (Do not modify these fields through Editor)")]
	public int health;

	private void Start() {
		health = maxHealth;
		RefreshUI();
	}

	private void RefreshUI() {
		if (healthBar) {
			var ratio = (float) health;
			ratio /= maxHealth;
			var scale = healthMask.localScale;
			scale.x = ratio;
			healthMask.localScale = scale;
			var leftmost = healthBar.localPosition;
			leftmost.x -= (healthBar.localScale.x - healthBar.localScale.x * ratio) / 2f;
			leftmost.y = healthMask.localPosition.y;
			leftmost.z = -1f;
			healthMask.localPosition = leftmost;
		}
	}

	public void OnBulletHit(int damage) {
		// reduce health
		health -= damage;
		// when health is negative, call onDeath event if it is not null
		if (health <= 0) onDeath?.Invoke(); // equals to if (onDeath != null) onDeath.Invoke();
		else RefreshUI();
	}

	public void SelfDestruct() {
		if (deathSfx != null) {
			AudioManager.PlayAtPoint(deathSfx, transform.position);
		}
		
		Destroy(gameObject);
	}
}
