using System;
using UnityEngine;

public class BasicEnemyMovement : MonoBehaviour {

	[Header("Settings")]
	public int score = 1;
	public int edge;
	public float speed;
	public Transform player;
	public Transform playerBase;
	public TargetStrategy strategy;
	public float stopDistance = 1f;
	public Rigidbody2D rigidbody;

	[Header("Status (Do not modify these fields through Editor)")]
	public Transform currentTarget;
	public float distanceSqrToTarget;

	public void Start() {
		if (TryGetComponent(out Health health)) {
			health.onDeath.AddListener(AddScore);
		}
	}

	public void AddScore() {
		if (WaveGenerator.instance) WaveGenerator.AddScore(score);
	}

	public void Update() {
		FindTarget();
		if (currentTarget == null) return;
		float stopDistanceSqr = stopDistance * stopDistance;
		// If it is away from the stop distance, move to the target with assigned velocity.
		if (stopDistanceSqr < distanceSqrToTarget) rigidbody.velocity = (currentTarget.position - transform.position).normalized * speed;
		else rigidbody.velocity = Vector3.zero;
	}

	public void FindTarget() {
		bool nullPlayer = strategy == TargetStrategy.Player && player == null;
		bool nullBase = strategy == TargetStrategy.Base && playerBase == null;
		bool nullBoth = strategy == TargetStrategy.PlayerAndBase && player == null && playerBase == null;

		// If there is no available target, clear the target slot.
		if (nullPlayer || nullBase || nullBoth) {
			currentTarget = null;
			distanceSqrToTarget = 0;
			return;
		}

		// Find the closet available target.
		float distanceSqrToPlayer = player == null ? float.PositiveInfinity : (player.position - transform.position).sqrMagnitude;
		float distanceSqrToBase = playerBase == null ? float.PositiveInfinity : (playerBase.position - transform.position).sqrMagnitude;

		if (strategy == TargetStrategy.Player) {
			currentTarget = player;
			distanceSqrToTarget = distanceSqrToPlayer;
		} else if (strategy == TargetStrategy.Base) {
			currentTarget = playerBase;
			distanceSqrToTarget = distanceSqrToBase;
		} else {
			currentTarget = distanceSqrToPlayer < distanceSqrToBase ? player : playerBase;
			distanceSqrToTarget = Mathf.Min(distanceSqrToPlayer, distanceSqrToBase);
		}
	}
}

public enum TargetStrategy {
	PlayerAndBase,
	Player,
	Base
}