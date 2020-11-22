using System;
using UnityEngine;

public class MissileController : MonoBehaviour
{

    public string damageTag = "Enemy";
    public int damage;
    public float radius;

    [Header("Do not change these: ")]
    public GameObject target;
    public Rigidbody2D rigidbody;
    public float speed;
    
    void Update() {

        if (target != null)
        {
            
            if (Vector3.Distance(transform.position, target.transform.position) > radius)
            {
                target = null;
            }
            else
            {
                rigidbody.velocity = (target.transform.position - transform.position).normalized * speed;
            }
            
        }
        else
        {
            foreach (Collider2D col in Physics2D.OverlapCircleAll(transform.position, radius)) {
                if (!String.IsNullOrEmpty(damageTag) && col.CompareTag(damageTag)) {
                    target = col.gameObject;
                    
                } 
            }

        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        // the bullet disappears if it hits walls/obstacles or damages the object if it fits the damageTag
        if (other.gameObject.layer == LayerMask.NameToLayer("Environment") || (!String.IsNullOrEmpty(damageTag) && other.CompareTag(damageTag))) {
            if (other.TryGetComponent(out Health health)) {
                health.OnBulletHit(damage);
            }
			
            Destroy(gameObject);
        }
    }
    
}
