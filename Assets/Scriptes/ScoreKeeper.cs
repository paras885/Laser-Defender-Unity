using System.Collections;
using System.Collections.Generic;

public class ScoreKeeper {
	private static ScoreKeeper instance = null;
	private int score;
	public ScoreKeeper() {
		if (instance == null && instance != this) {
			score = 0;
			instance = this;
		}
	}

	public void updateScore(int update) {
		instance.score += update;
	}

	public int getScore() {
		return instance.score;
	}
}
