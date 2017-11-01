using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyLaser : MonoBehaviour {

	public float power = 100f;

	public float getPower () {
		return power;
	}

	void OnTriggerEnter2D (Collider2D collider) {
		// Player
		PlayerController playerController = collider.gameObject.GetComponent<PlayerController> ();
		if (playerController) {
			float remainHelath = playerController.updateAndGetHealth (power);
			if (remainHelath <= 0) {
				LevelManger levelManger = new LevelManger ();
				levelManger.loadLevel ("Lose_Screen");
			}
			Destroy (gameObject);
		} else {
			// Player's Laser
			PlayerLaser playerLaser = collider.gameObject.GetComponent<PlayerLaser> ();
			if (playerLaser) {
				Destroy (collider.gameObject);
				Destroy (gameObject);
			}
		}
	}
}
