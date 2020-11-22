using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdate : MonoBehaviour {

	public Text text;
	public PlayerController player;
	public int score;

	private void Start() {
		text = GetComponent<Text>();
		score = player.score;
	}

	private void Update() {
		if (player && score != player.score) {
			score = player.score;
			text.text = "Score: " + score;
		}
	}
}