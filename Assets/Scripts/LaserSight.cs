using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSight : MonoBehaviour
{
    public float laserBeamLength;
    private LineRenderer lineRenderer;
    private ParticleSystem particleSystem;
    private Vector3[] positions = new Vector3[2];
    
    // Start is called before the first frame update
    void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        particleSystem = GetComponent<ParticleSystem>();
        particleSystem.enableEmission = false;
    }

    public void OnTriggerPressed() {
        lineRenderer.enabled = true;
        particleSystem.enableEmission = true;
    }

    public void OnTriggerReleased() {
        lineRenderer.enabled = false;
        particleSystem.enableEmission = false;
    }

    // Update is called once per frame
    void Update() {
        Vector3 endPosition = transform.position + transform.up * laserBeamLength;
        positions[0] = transform.position;
        positions[1] = endPosition;
        lineRenderer.SetPositions(positions);
    }
}
