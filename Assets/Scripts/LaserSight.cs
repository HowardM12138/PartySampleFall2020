using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSight : MonoBehaviour
{
    public float laserBeamLength;
    private LineRenderer lineRenderer;
    private ParticleSystem particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        particleSystem = GetComponent<ParticleSystem>();
        particleSystem.enableEmission = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            lineRenderer.enabled = true;
            particleSystem.enableEmission = true;
        }
        
        if(Input.GetButtonUp("Fire1"))
        {
            lineRenderer.enabled = false;
            particleSystem.enableEmission = false;
        }
        Vector3 endPosition = transform.position + (transform.up * laserBeamLength);
        lineRenderer.SetPositions(new Vector3[] {transform.position, endPosition});


    }
}
