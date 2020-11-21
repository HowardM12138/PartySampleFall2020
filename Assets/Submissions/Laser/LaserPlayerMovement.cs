using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaserPlayerMovement : MonoBehaviour {

	[Header("Settings")]
	public int edge = 5;
	public float speed;
	public KeyCode upKey = KeyCode.W;
	public KeyCode downKey = KeyCode.S;
	public KeyCode leftKey = KeyCode.A;
	public KeyCode rightKey = KeyCode.D;
	public KeyCode fireKey = KeyCode.Mouse0;
	public KeyCode restartKey = KeyCode.R;

	[Header("Configurations")]
	public Rigidbody2D rigidbody;
	public LaserController weapon;
	
	[Header("Status (Do not modify these fields through Editor)")]
	public float velocityX;
	public float velocityY;

	private void Start() => Application.targetFrameRate = 60;

	private void Update() {
		if (Input.GetKey(restartKey)) OnDeath();
		
		var vertical = 0f;
		var horizontal = 0f;
			
		if (Input.GetKey(upKey)) vertical = speed;
		else if (Input.GetKey(downKey)) vertical = -speed;
			
		if (Input.GetKey(leftKey)) horizontal = -speed;
		else if (Input.GetKey(rightKey)) horizontal = speed;

		if (vertical != 0 && horizontal != 0) {
			velocityX = horizontal / Mathf.Sqrt(2f);
			velocityY = vertical / Mathf.Sqrt(2f);
		} else {
			velocityX = horizontal;
			velocityY = vertical;
		}
			
		rigidbody.velocity = new Vector2(velocityX, velocityY);
		
		var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		var rotZ = Quaternion.LookRotation(weapon.transform.position - mousePos, Vector3.forward).eulerAngles.z;
		weapon.transform.rotation = Quaternion.Euler(0, 0, rotZ);

		//if (Input.GetKey(fireKey)) weapon.Trigger();
	}

	public void OnDeath() {
		Debug.Log("YOU DIED !");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}