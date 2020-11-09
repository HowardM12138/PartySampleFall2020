using System;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{
    public string damageTag = "Player";
    public int damage;
    public float timeToExplode;
    public float radius;

    [Header("Do not change these: ")]
    public float timeStart;
    public float currentTime;
    
    private void Awake()
    {
        timeStart = Time.time;
    }

    private void Update()
    {
        currentTime = Time.time;
        if (currentTime - timeStart >= timeToExplode)
        {
            Explode();    
        }
    }

    private void Explode()
    {
        foreach (Collider2D col in Physics2D.OverlapCircleAll(transform.position, radius))
        {
            if (col.CompareTag(damageTag))
            {
                if (col.TryGetComponent(out Health health)) {
                    health.OnBulletHit(damage);
                }
            } 
        }
        
        Destroy(gameObject);
    }
}