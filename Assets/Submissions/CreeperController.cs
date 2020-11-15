using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreeperController : MonoBehaviour
{
    
    [Header("Settings")]
    public float slowSpeed;
    public float sneakSpeed;
    public float range;
    [Range(0f, 1f)]
    public float aggressiveness;
    public float radius;

    [Header("Configurations")]
    public Transform pathStart;
    public Transform pathEnd;
    public WeaponController weapon;

    [Header("Status (Do not modify these fields through Editor)")]
    public Transform to;
    public GameObject target;

    // Start is called before the first frame update
    private void Start() {
        to = pathEnd;
        transform.position = pathStart.position;
        target = GameObject.FindWithTag("Player");
    }


    // Update is called once per frame
    private void Update()
    {
        float speed;
        if (to) {
            if (Vector3.Distance(transform.position, target.transform.position) <= radius)
            {
                speed = sneakSpeed;
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

        if (target && weapon) {
            var targetPos = target.transform.position;
            var fwd = weapon.transform.position - targetPos;
            fwd.y *= -1f;
            var rotZ = Quaternion.LookRotation(fwd, Vector3.forward).eulerAngles.z;
            weapon.transform.rotation = Quaternion.Euler(0, 0, rotZ);
            if ((targetPos - transform.position).sqrMagnitude <= range * range) {
                if (!Physics2D.Linecast(transform.position, targetPos, LayerMask.GetMask("Environment")).collider) {
                    if (weapon.CanFire()) {
                        if (Random.value < aggressiveness) {
                            weapon.Trigger();
                        }
                    }
                }
            }
        }
    }
}
