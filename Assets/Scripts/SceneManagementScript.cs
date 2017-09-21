using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneManagementScript : MonoBehaviour {
	public static SceneManagementScript main;
	public GameObject mainMenuPanel, playInstructionPanel;
	public GameObject pausePanel;
	public GameObject leftRacket, rightRacket;
	public bool isMainMenu;
	public GameObject scorePanel;
	public TextMeshProUGUI winText;
	public bool twoPlayerMode;

	void Awake(){
		main = this;
		mainMenuPanel.SetActive(true);
		pausePanel.SetActive(false);
		playInstructionPanel.SetActive(false);
		scorePanel.SetActive(true);
		leftRacket.GetComponent<RacketControllerScript>().isAI = true;
		rightRacket.GetComponent<RacketControllerScript>().isAI = true;
	}

	// Use this for initialization
	void Start () {
		isMainMenu = true;
		twoPlayerMode = false;
	}

	public void DeclareWinner(string winner){
		scorePanel.SetActive(false);
		winText.gameObject.SetActive(true);
		if(winner.Equals("right racket")){
			winText.text = "right racket wins";
		}
		else{
			winText.text = "left racket wins";
		}

		StartCoroutine(WaitFor(3f));
	}

	private IEnumerator WaitFor(float t){
		yield return new WaitForSeconds(t);
		SceneManager.LoadScene(0);

	}

}
