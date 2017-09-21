using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ButtonActionScript : MonoBehaviour {
	public Color purple;
	public Color green;
	public Texture2D cursorTexture;
	private bool pause;
	public GameObject audioObjOn, audioObjOff;

	// Use this for initialization
	void Start () {
		pause = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){ 
			PauseOrPlay();
		}
		
	}

	//Called when there's a mouse hover within button region
	public void OnPointerEnter(Transform button){
		//Cursor.SetCursor(cursorTexture, new Vector2(0.01f, 0.01f), CursorMode.Auto);
		button.GetChild(0).gameObject.SetActive(true);
		button.GetChild(5).GetComponent<TextMeshProUGUI>().color = green;
	}

	public void OnPointerExit(Transform button){
		//Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
		button.GetChild(0).gameObject.SetActive(false);
		button.GetChild(5).GetComponent<TextMeshProUGUI>().color = Color.white;
	}

	public void ToggleFullScreen(){
		 Screen.fullScreen = !Screen.fullScreen;
	}

	public void StartGame(string mode){
		SceneManagementScript.main.isMainMenu = false;
		SceneManagementScript.main.twoPlayerMode = false;
		SceneManagementScript.main.mainMenuPanel.SetActive(false);
		SceneManagementScript.main.playInstructionPanel.SetActive(true);
		BallScript.main.ResetSpeed();
		ScoreScript.main.ResetScore();
		SceneManagementScript.main.rightRacket.GetComponent<RacketControllerScript>().isAI = false;
		GameObject.Find("Right Racket").GetComponent<RacketControllerScript>().isAI = false;

		if(mode.Equals("Two Players")){
			SceneManagementScript.main.twoPlayerMode = true;
			GameObject.Find("Left Racket").GetComponent<RacketControllerScript>().isAI = false;
		}

		foreach(var racket in BallScript.main.rackets){
			racket.GetComponent<RacketControllerScript>().MoveRacketToYaxisCenter();
		}

		BallScript.main.FadeOutAndResetBallPos();

	}

	public void ShareToTwitter(){
		return;
		string text = "PONG | A WebGL Experiment by @kolapo_";
		string url = "http://kolapo.me/pong-test.github.io/";
    	Application.OpenURL("http://twitter.com/intent/tweet" + "?text=" + WWW.EscapeURL(text + "\n") +"&amp;url=" + WWW.EscapeURL(url) +"&amp;lang=" + WWW.EscapeURL("en"));
	}

	public void ViewDevelopersTwitterProfile(){
		Application.OpenURL("http://twitter.com/kolapo_"); 
	}

	public void ViewCodeOnGithub(){
		Application.OpenURL("https://github.com/thekolapo"); 
	}

	public void PauseOrPlay(){
		if(SceneManagementScript.main.isMainMenu){
			return;
		}
		pause = !pause; 
		Time.timeScale = (pause == true)? 0 : 1;
		SceneManagementScript.main.pausePanel.SetActive(pause);
	}

	public void ReturnToMainMenu(){
		Time.timeScale = 1;
		SceneManager.LoadScene(0);
	}

	public void Restart(){ 
		SceneManagementScript.main.pausePanel.SetActive(false);
		foreach(Transform racket in BallScript.main.rackets){
			racket.GetComponent<RacketControllerScript>().MoveRacketToYaxisCenter();
		}
		pause = false;
		BallScript.main.ResetBallPosition(true);
		ScoreScript.main.ResetScore();
		Time.timeScale = 1;
	}

	public void ToggleAudio(){
		if(audioObjOn.activeInHierarchy){
			audioObjOn.SetActive(false);
			audioObjOff.SetActive(true);
		}
		else{
			audioObjOff.SetActive(false);
			audioObjOn.SetActive(true);
		}

		AudioScript.main.ToggleAudio();
	}


}
