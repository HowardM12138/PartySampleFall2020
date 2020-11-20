using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadPlayerMovement : MonoBehaviour {

	[Header("Settings")]
	public float speed;
	public float MaxHorizDist;
	public float MaxVertDist;
	public KeyCode upKey = KeyCode.W;
	public KeyCode downKey = KeyCode.S;
	public KeyCode leftKey = KeyCode.A;
	public KeyCode rightKey = KeyCode.D;
	
	[Header("Status (Do not modify these fields through Editor)")]
	public float moveX;
	public float moveY;

	private void Update() {
		
		var vertical = 0f;
		var horizontal = 0f;

		// Cardinal Movement	
		if (Input.GetKey(upKey)) {
			vertical = speed;
		} else if (Input.GetKey(downKey)) {
			vertical = -speed;
		}
		if (Input.GetKey(leftKey)) {
			horizontal = -speed;
		} else if (Input.GetKey(rightKey)) {
			horizontal = speed;
		}

		// Diagonal Movement + Vector Component Assignment
		if (vertical != 0 && horizontal != 0) {
			moveX = horizontal / Mathf.Sqrt(2f);
			moveY = vertical / Mathf.Sqrt(2f);
		} else {
			moveX = horizontal;
			moveY = vertical;
		}

		// Updates Dead Player's Position
		transform.position = transform.position + new Vector3(moveX, moveY, 0);
	
		// Teleports Dead Player Back to Origin
		if (Math.Abs(transform.position.x) > MaxHorizDist || Math.Abs(transform.position.y) > MaxVertDist){
			transform.position = new Vector3(0, 0, 0);
		}
	
	}
}

