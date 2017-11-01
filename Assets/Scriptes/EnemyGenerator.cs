using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width;
	public float height;
	public float enemySpeed;

	public int gameCount;

	private float minX;
	private float maxX;
	private float directionSideForX;

	// Use this for initialization
	void Start () {
		gameCount = 1;
		float zDistance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, zDistance));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, zDistance));
		minX = leftMost.x;
		maxX = rightMost.x;
		directionSideForX = 1.0f;
		generateEnemy (gameCount);
	}

	void generateEnemy (int GameCount) {
		foreach (Transform child in transform) {
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
			enemy.gameObject.GetComponent<enemy> ().probSpace = GameCount;
		}
	}

	void OnDrawGizmos () {
		Gizmos.DrawWireCube (transform.position, new Vector3 (width, height));
	}
	
	// Update is called once per frame
	void Update () {
		if (isNeedToChangeDirection ()) {
			directionSideForX *= -1.0f;
		}
		if (directionSideForX > 0f) {
			transform.position += Vector3.left * Time.deltaTime * enemySpeed;
		} else {
			transform.position += Vector3.right * Time.deltaTime * enemySpeed;
		}
		if (AllEnemyDead ()) {
			generateEnemy (++gameCount);
		}
	}

	private bool AllEnemyDead () {
		foreach (Transform child in transform) {
			if (child.childCount > 0) {
				return false;
			}
		}
		return true;
	}

	private bool isNeedToChangeDirection () {
		float leftPoint = transform.position.x - (0.5f * width);
		float rightPoint = transform.position.x + (0.5f * width);
		if (leftPoint < minX) {
			return directionSideForX > 0f;
		} else if (rightPoint > maxX) {
			return directionSideForX < 0f;
		}
		return false;
	}
}
