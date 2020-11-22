using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreeperController : MonoBehaviour
{
    
    [Header("Settings")]
    public float slowSpeed;
    public float sneakSpeed;
    public float encounterTime;
    //public float range;
    [Range(0f, 1f)]
    //public float aggressiveness;
    public float radius;

    [Header("Configurations")]
    public Transform pathStart;
    public Transform pathEnd;

    [Header("Status (Do not modify these fields through Editor)")]
    public Transform to;
    public GameObject target;
    public float firstEncounterTime;

    // Start is called before the first frame update
    private void Start() {
        to = pathEnd;
        transform.position = pathStart.position;
        target = GameObject.FindWithTag("Player");
        firstEncounterTime = 0;
    }


    // Update is called once per frame
    private void Update()
    {
        float speed;
        
        //check timer
        if (Time.time - firstEncounterTime >= encounterTime)
        {
            GetComponent<CreeperHealth>().Explode();
        }
        
        if (to) {
            if (Vector3.Distance(transform.position, target.transform.position) <= radius)
            {
                speed = sneakSpeed;
                if (firstEncounterTime == 0)
                {
                    firstEncounterTime = Time.time;
                }
            }
            else
            {
                speed = slowSpeed;
            }

            transform.position = Vector3.MoveTowards(transform.position, to.position, speed * Time.deltaTime);
            if (transform.position == to.transform.position) {
                to = to == pathStart ? pathEnd : pathStart;
            }
        }
    }
}
