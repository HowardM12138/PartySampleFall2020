using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZolygonDamage : MonoBehaviour{
    public string damageTag = "Player";
	public int damage;
    //public float hitRate = 1f;
    //public float attackRadius = 0.001f;

    private bool collision = false;

    [Header("Status (Do not modify these fields through Editor)")]
    public GameObject target;
    
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == damageTag){
            collision = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag == damageTag){
            collision = false;
        }
    }

    private void Start() {
        target = GameObject.FindGameObjectWithTag(damageTag);
    }

    private void Update() {
        if(collision == true){
            if (target.TryGetComponent(out Health health)) {
                health.OnBulletHit(damage);
            }
        }
    }
}

// This code does the same. Pause does not work

/*
    private void Start() {
        target = GameObject.FindGameObjectWithTag(damageTag);
    }

    private void Update() {
        if(InRange()){
            StartCoroutine("DamagePlayer");
        }
    }

    IEnumerator DamagePlayer(){
        if (target.TryGetComponent(out Health health)) {
            health.OnBulletHit(damage);
            yield return new WaitForSeconds(hitRate);
        }
    }

    public bool InRange(){
        var targetPos = target.transform.position;
        var zolygonPos = transform.position;
        float distance = Vector2.Distance(targetPos, zolygonPos);
        return distance <= attackRadius; 
    }
}

*/