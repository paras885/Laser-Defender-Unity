using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {

	public float health = 150f;
	public float projectileSpeed;
	public int probSpace;

	public AudioClip dieSound;
	public AudioClip laserSound;

	public GameObject enemyLaserPrefab;

	private Animator animator;

	void Start() {
		animator = GetComponent<Animator> ();
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (!animator.GetBool ("IsEnemyAlive")) {
			return;
		}

		PlayerLaser playerLaser = collider.gameObject.GetComponent<PlayerLaser> ();
		if (playerLaser) {
			health -= playerLaser.getPower ();
			if (health <= 0) {
				ScoreKeeper scoreKeeper = new ScoreKeeper ();
				scoreKeeper.updateScore (probSpace);

				animator.SetBool ("IsEnemyAlive", false);
			}
			Destroy (collider.gameObject);
		}
	}

	void Update () {
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Die")) {
			Die ();
			return;
		}
		if (NeedToFireLaser ()) {
			Vector3 tweak = new Vector3 (0f, -0.2f, 0f);
			GameObject enemyLaser = Instantiate (enemyLaserPrefab, transform.position + tweak, Quaternion.identity);
			enemyLaser.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, -projectileSpeed);
			AudioSource.PlayClipAtPoint (laserSound, transform.position);
		}
	}

	private bool NeedToFireLaser () {
		int randomValue = Random.Range (1, 100);
		return randomValue <= probSpace;
	}

	public void Die() {
		AudioSource.PlayClipAtPoint (dieSound, Camera.main.transform.position);
		Destroy (gameObject);
	}
}
