using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    public GameObject shoppingMenu;
    public GameObject playerBase;
    public GameObject player;
    public int radius;
    public bool canShop;
    public int playerHealthRegainPrice = 300;
    public int baseHealthRegainPrice = 300;

    private Health health;

    // Start is called before the first frame update
    void Start()
    {
        shoppingMenu.SetActive(false);
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update() {
        if (canShop) {
            if (Input.GetKeyUp(KeyCode.Alpha9)) {
                RegainPlayerHealth();
            }

            if (Input.GetKeyUp(KeyCode.Alpha0)) {
                RegainBaseHealth();
            }
        }
        
        foreach (Collider2D col in Physics2D.OverlapCircleAll(playerBase.transform.position, radius)) {
            if (col.Equals(player.GetComponent<Collider2D>())) {
                if (shoppingMenu != null) {
                    shoppingMenu.SetActive(true);
                    canShop = true;
                }
                // Debug.Log("hi");
                return;
            } 
        }

        // Debug.Log("goodbye");

        if (shoppingMenu != null) {
            shoppingMenu.SetActive(false);
            canShop = false;
        }
    }

    public void RegainPlayerHealth() {
        var p = player.GetComponent<PlayerController>();
        if (p.score >= playerHealthRegainPrice && p.health.health < p.health.maxHealth) {
            p.score -= playerHealthRegainPrice;
            p.health.health += 1;
            p.health.OnBulletHit(0);
        }
    }

    public void RegainBaseHealth() {
        var p = player.GetComponent<PlayerController>();
        if (p.score >= baseHealthRegainPrice && health.health < health.maxHealth) {
            p.score -= baseHealthRegainPrice;
            health.health += 10;
            health.OnBulletHit(0);
        }
    }
}
