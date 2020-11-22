using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZolygonDamage : MonoBehaviour{
    public string damageTag = "Player";
	public int damage;
    public float attackInterval = 1f;
    public SpriteRenderer renderer;
    public Color cooldownColor = Color.blue;

    private bool collision;

    [Header("Status (Do not modify these fields through Editor)")]
    public Health target;
    public float lastAttackTime;
    public bool canAttack = true;
    public Color originalColor;
    
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag(damageTag)) {
            other.gameObject.TryGetComponent(out Health health);
            if (health) {
                target = health;
            }
            
            collision = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag(damageTag)) {
            collision = false;
        }
    }

    private void Start() {
        originalColor = renderer.color;
    }

    private void Update() {
        if (collision && canAttack) {
            if (target) {
                target?.OnBulletHit(damage);
                lastAttackTime = Time.time;
                canAttack = false;
                renderer.color = cooldownColor;
            }
        }

        if (Time.time - lastAttackTime > attackInterval) {
            canAttack = true;
            renderer.color = originalColor;
        }
    }
}