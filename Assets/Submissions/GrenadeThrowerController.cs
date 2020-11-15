using System;
using UnityEngine;

public class GrenadeThrowerController : MonoBehaviour
{
        
        [Header("Settings")]
        public int damage;
        public float bulletSpeed;
        public float fireInterval = .5f;
        public string damageTag = "Enemy";

        [Header("Configurations")]
        public Transform fireOrigin;
        public GameObject grenadePrototype;
        
        [Header("Debug")] 
        public KeyCode throwKey;

        [Header("Status (Do not modify these fields through Editor)")]
        public float lastFireTime;
        public void Update()
        {
                if (Input.GetKey(throwKey))
                {
                        Trigger();
                }
        }

        public void Trigger()
        {
                
                if (CanFire())
                {
                        Fire();
                }
        }

        public bool CanFire()
        {
                var time = Time.time;
                return lastFireTime + fireInterval <= time;
        }

        private void Fire()
        {
                Debug.Log("Firing");
                lastFireTime = Time.time;
                var grenade = Instantiate(grenadePrototype, fireOrigin ? fireOrigin.position : transform.position, transform.rotation);
                var controller = grenade.GetComponent<GrenadeController>();
                controller.damage = damage;
                controller.damageTag = damageTag;
                var velocity = grenade.transform.up * bulletSpeed;
                controller.GetComponent<Rigidbody2D>().velocity = velocity;
        }

}
