using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBulletController : MonoBehaviour
{   
    SpriteRenderer rend;
    private float fadeTime = 0.5f;
    private float time = 0f;
    public string damageTag = "Player";
	public int damage;
	
	private WaitForSeconds wait = new WaitForSeconds(0.05f);

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    IEnumerator FadeOut()
    {
        for (float f = 1f; f >= -0.05f; f -= 0.05f)
        {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return wait;
        }
        Destroy(gameObject);
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time >= fadeTime)
        {
           StartCoroutine("FadeOut");
           time = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer == LayerMask.NameToLayer("Environment") || other.CompareTag(damageTag)) {
			if (other.TryGetComponent(out Health health)) {
				health.OnBulletHit(damage);
			}
		}
	}
}
