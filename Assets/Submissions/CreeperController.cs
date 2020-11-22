using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class CreeperController : MonoBehaviour
{
    public float encounterTime;
    public float radius;
    public int damage;
    private float firstEncounterTime;
    private BasicEnemyMovement movementController;
    private float slowSpeed;
    

    // Start is called before the first frame update
    private void Start() {
        firstEncounterTime = 0;
        GetComponent<Health>().onDeath.AddListener(Explode);
        BasicEnemyMovement movementController = GetComponent<BasicEnemyMovement>();
        slowSpeed = movementController.speed;
    }


    // Update is called once per frame
    private void Update()
    {
        
        //check timer
        if (Time.time - firstEncounterTime >= encounterTime)
        {
            Explode();
        }

        if (movementController != null)
        {
            if (movementController.distanceSqrToTarget <= radius * radius)
            {
                movementController.speed = slowSpeed * 2;
                if (firstEncounterTime == 0)
                {
                    firstEncounterTime = Time.time;
                }
            } 
            else
            {
                movementController.speed = slowSpeed;
            }   
        }

    }
    
    public void Explode()
    {
        foreach (Collider2D col in Physics2D.OverlapCircleAll(transform.position, radius))
        {
            if (!(col.gameObject == gameObject) && (col.CompareTag("Player") || col.CompareTag("Enemy")))
            {
                if (col.TryGetComponent(out Health health)) {
                    health.OnBulletHit(damage);
                }
            } 
        }
        
        Destroy(gameObject);
        
    }
    
}
