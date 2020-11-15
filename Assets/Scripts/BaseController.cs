using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    public GameObject shoppingMenu;
    public GameObject playerBase;
    public GameObject player;
    public int radius;

    // Start is called before the first frame update
    void Start()
    {
        shoppingMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Collider2D col in Physics2D.OverlapCircleAll(playerBase.transform.position, radius))
        {
            if (col.Equals(player.GetComponent<Collider2D>()))
            {
                if (shoppingMenu != null)
                {
                    shoppingMenu.SetActive(true);
                }
                Debug.Log("hi");
                return;
            } 
        }

        Debug.Log("goodbye");

        if (shoppingMenu != null)
        {
            shoppingMenu.SetActive(false);
        }
    }

}
