using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManger : MonoBehaviour {

	public void loadLevel(string levelName) {
		Debug.Log ("Level Change request for : " + levelName);
		SceneManager.LoadScene (levelName);
	}
}
