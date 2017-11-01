using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePublisher : MonoBehaviour {

	public Text scoreLabel;

	// Use this for initialization
	void Start () {
		ScoreKeeper scoreKeeper = new ScoreKeeper ();
		scoreLabel.text = scoreKeeper.getScore ().ToString();
		scoreKeeper.updateScore (-1 * scoreKeeper.getScore ());
	}
}
