using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float padding;
	public float projectileSpeed;
	public float firingSpeed;
	public float health;

	public AudioClip dieClip; 

	public Text scoreText;

	public GameObject laserBulletPrefab;

	private float minX;
	private float maxX;

	// Use this for initialization
	void Start () {
		float zDistance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, zDistance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, zDistance));
		minX = leftMost.x + padding;
		maxX = rightMost.x - padding;
	}

	void Fire () {
		GameObject projectile = Instantiate (laserBulletPrefab, transform.position, Quaternion.identity) as GameObject;
		projectile.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, projectileSpeed);
		GetComponent<AudioSource> ().Play ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			InvokeRepeating ("Fire", 0.000001f, firingSpeed);
		} 
		if (Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke ("Fire");
		}
		// Only x direction change
		float horizontal = Input.GetAxisRaw ("Horizontal");
		Vector3 direction = new Vector3 (horizontal, 0f, 0f);
		this.gameObject.transform.Translate (direction * speed * Time.deltaTime);
		transform.position = new Vector3 (Mathf.Clamp (transform.position.x, minX, maxX), 
			                              transform.position.y, transform.position.z);
		ScoreKeeper scoreKeeper = new ScoreKeeper ();
		int score = scoreKeeper.getScore ();
		scoreText.text = string.Format ("{0}", score);
	}

	public float updateAndGetHealth (float damage) {
		health -= damage;
		return health;
	}
}
