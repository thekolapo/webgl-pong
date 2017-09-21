using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreScript : MonoBehaviour {
	public static ScoreScript main;
	public TextMeshProUGUI leftRacketScoreText, rightRacketScoreText;

	void Awake(){
		main = this;
	}

	public void UpdateScore (string whichRacket) {
		switch (whichRacket)
		{
		case "Left Racket":
			leftRacketScoreText.text = (int.Parse (leftRacketScoreText.text) + 1).ToString();
			break;

		case "Right Racket":
			rightRacketScoreText.text = (int.Parse (rightRacketScoreText.text) + 1).ToString();
			break;
		}

		if(SceneManagementScript.main.leftRacket.GetComponent<RacketControllerScript>().isAI && SceneManagementScript.main.rightRacket.GetComponent<RacketControllerScript>().isAI){
			return;
		}

		if(leftRacketScoreText.text == "5" || rightRacketScoreText.text == "5"){
			SceneManagementScript.main.DeclareWinner((leftRacketScoreText.text.Equals("5")) ? "left racket" : "right racket");
			ResetScore();
		}

	}

	public void ResetScore(){
		leftRacketScoreText.text = 0.ToString();
		rightRacketScoreText.text = 0.ToString();
	}

}
